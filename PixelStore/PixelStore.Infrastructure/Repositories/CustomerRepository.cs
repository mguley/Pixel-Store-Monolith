using Microsoft.EntityFrameworkCore;
using PixelStore.Domain.Customer;
using PixelStore.Infrastructure.Exceptions.Customer;

namespace PixelStore.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="Customer"/> entity.
/// </summary>
internal sealed class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used by the repository.</param>
    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// Retrieves a customer by its unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the customer to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>The customer if found; otherwise null.</returns>
    public async Task<Customer?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Customer>()
            .FirstOrDefaultAsync(predicate: customer => customer.Guid == guid, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="entity">A customer to be updated.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>True if success.</returns>
    /// <exception cref="CustomerOperationException">Thrown if the customer is not found.</exception>
    public async Task<bool> UpdateAsync(Customer entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Customer? customer = await DbContext.Set<Customer>()
            .FindAsync(keyValues: new object[] { entity.Guid }, cancellationToken: cancellationToken);

        if (customer is null)
        {
            throw new CustomerOperationException(message: $"Customer with ID { entity.Guid } not found.");
        }
        
        DbContext.Entry(entity: customer).CurrentValues.SetValues(obj: entity);
        return true;
    }

    /// <summary>
    /// Deletes a customer by their unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the customer to delete.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>True if the customer was found and removed; otherwise, throws a <see cref="CustomerOperationException"/>.</returns>
    /// <exception cref="CustomerOperationException">Thrown when the customer cannot be found.</exception>
    public async Task<bool> DeleteByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        Customer? customer = await DbContext.Set<Customer>()
            .FirstOrDefaultAsync(predicate: customer => customer.Guid == guid, cancellationToken: cancellationToken);

        if (customer is null)
        {
            throw new CustomerOperationException(message: $"Customer with ID { guid } not found.");
        }

        DbContext.Set<Customer>().Remove(entity: customer);
        return true;
    }
}
