using Microsoft.EntityFrameworkCore;
using PixelStore.Domain.Address;
using PixelStore.Infrastructure.Exceptions.Address;

namespace PixelStore.Infrastructure.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="Address"/> entity.
/// </summary>
internal sealed class AddressRepository : Repository<Address>, IAddressRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddressRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used by the repository.</param>
    public AddressRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    /// <summary>
    /// Retrieves an address by its unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the address to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>The address if found; otherwise null.</returns>
    public async Task<Address?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Address>()
            .FirstOrDefaultAsync(predicate: address => address.Guid == guid, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Updates an existing address.
    /// </summary>
    /// <param name="entity">An address to be updated.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>True if success.</returns>
    /// <exception cref="AddressOperationException">Thrown if the address is not found.</exception>
    public async Task<bool> UpdateAsync(Address entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Address? address = await DbContext.Set<Address>()
            .FindAsync(keyValues: new object[] { entity.Guid }, cancellationToken: cancellationToken);

        if (address is null)
        {
            throw new AddressOperationException(message: $"Address with ID { entity.Guid } not found.");
        }
        
        DbContext.Entry(entity: address).CurrentValues.SetValues(obj: entity);
        return true;
    }

    /// <summary>
    /// Deletes an address by their unique identifier.
    /// </summary>
    /// <param name="guid">The globally unique identifier of the address to delete.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>True if the address was found and deleted; otherwise, throws a <see cref="AddressOperationException"/>.</returns>
    /// <exception cref="AddressOperationException">Thrown when the address cannot be found.</exception>
    public async Task<bool> DeleteByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        Address? address = await DbContext.Set<Address>()
            .FirstOrDefaultAsync(predicate: address => address.Guid == guid, cancellationToken: cancellationToken);

        if (address is null)
        {
            throw new AddressOperationException(message: $"Address with ID { guid } not found.");
        }

        DbContext.Set<Address>().Remove(entity: address);
        return true;
    }
}
