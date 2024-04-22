using FluentValidation;
using FluentValidation.Results;
using MediatR;
using PixelStore.Application.Abstractions.Messaging.Command;
using PixelStore.Application.Exceptions;
using ValidationException = PixelStore.Application.Exceptions.ValidationException;

namespace PixelStore.Application.Abstractions.Behaviors;

/// <summary>
/// A behavior that intercepts handling of command messages to validate them before executing the handler.
/// This behavior utilizes FluentValidation validators to enforce business rules or data integrity.
/// </summary>
/// <typeparam name="TRequest">The type of the command that is being handled, must implement <see cref="IBaseCommand"/>.</typeparam>
/// <typeparam name="TResponse">The type of the response from the handler.</typeparam>
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest,TResponse}"/> class
    /// with the specified validators.
    /// </summary>
    /// <param name="validators">The validators to be used for validating the request.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
    
    /// <summary>
    /// Pipeline handler that intercepts the request to perform validation before the request is handled
    /// by its respective handler.
    /// </summary>
    /// <param name="request">The request message to handle.</param>
    /// <param name="next">Delegate to the next action in the pipeline.</param>
    /// <param name="cancellationToken">Cancellation token for cancelling the operation.</param>
    /// <returns>The response from the next action in the pipeline if the validation is successful;
    /// otherwise, throws an exception.</returns>
    /// <exception cref="ValidationException">Thrown when the validation fails
    /// with a collection of <see cref="ValidationError"/>.</exception>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        IValidationContext context = new ValidationContext<TRequest>(instanceToValidate: request);

        Func<ValidationResult, bool> filterValidResults = result => result.Errors.Any();
        Func<ValidationFailure, ValidationError> convertToValidationError = failure =>
        {
            return new ValidationError(failure.PropertyName, failure.ErrorMessage);
        };
        Func<IValidator<TRequest>, ValidationResult> validate = validator => validator.Validate(context: context);

        List<ValidationError> validationResults = _validators
            .Select(selector: validate)
            .Where(predicate: filterValidResults)
            .SelectMany(selector: validationResult => validationResult.Errors)
            .Select(selector: convertToValidationError)
            .ToList();

        if (validationResults.Any())
        {
            throw new ValidationException(errors: validationResults);
        }

        return await next();
    }
}
