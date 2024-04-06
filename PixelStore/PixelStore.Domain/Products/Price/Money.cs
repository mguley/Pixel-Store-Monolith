using PixelStore.Domain.Exceptions;

namespace PixelStore.Domain.Products.Price;

/// <summary>
/// Represents a monetary value in a specific currency.
/// </summary>
public sealed record Money(decimal Amount, Currency Currency)
{
    private const int DefaultZeroValue = 0;
    
    /// <summary>
    /// Adds two instances of Money, provided they are in the same currency.
    /// </summary>
    /// <param name="first">The first money amount.</param>
    /// <param name="second">The second money amount.</param>
    /// <returns>A new Money instance representing the sum of the two amounts.</returns>
    /// <exception cref="InvalidCurrencyException">Thrown when the currencies of the two amounts do not match.</exception>
    public static Money operator +(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new InvalidCurrencyException(message: "Currencies mismatch.");
        }

        return new Money(Amount: first.Amount + second.Amount, Currency: first.Currency);
    }

    /// <summary>
    /// Creates a zero-value Money instance in no specific currency.
    /// </summary>
    public static Money Zero() => new(Amount: DefaultZeroValue, Currency: Currency.None);

    /// <summary>
    /// Creates a zero-value Money instance in a specified currency.
    /// </summary>
    /// <param name="currency">The currency of the zero-value Money.</param>
    public static Money Zero(Currency currency) => new(Amount: DefaultZeroValue, Currency: currency);

    /// <summary>
    /// Determines if this instance represents a zero value.
    /// </summary>
    /// <returns>True if the Money amount is zero; otherwise, false.</returns>
    public bool IsZero() => this == Zero(currency: Currency);
}
