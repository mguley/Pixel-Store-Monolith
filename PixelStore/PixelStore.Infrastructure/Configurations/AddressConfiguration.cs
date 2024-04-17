using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStore.Domain.Address;
using PixelStore.Domain.Users;

namespace PixelStore.Infrastructure.Configurations;

/// <summary>
/// Configures the entity model for the Address entity.
/// </summary>
internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    /// <summary>
    /// Configures the entity model for the Address entity using the Fluent API.
    /// </summary>
    /// <param name="builder">Provides a simple API for configuring an EntityType.</param>
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(name: "addresses");

        builder.HasKey(address => address.Id);

        builder.Property(address => address.Id)
            .HasColumnName(name: "Id")
            .ValueGeneratedOnAdd();

        builder.Property(address => address.Guid)
            .HasColumnName(name: "guid");
        
        builder.Property(address => address.Street)
            .HasMaxLength(maxLength: 255)
            .HasConversion(
                street => street.Value,
                value => new Street(value))
            .HasColumnName(name: "street");

        builder.Property(address => address.City)
            .HasMaxLength(maxLength: 100)
            .HasConversion(
                city => city.Value,
                value => new City(value))
            .HasColumnName(name: "city");

        builder.Property(address => address.State)
            .HasMaxLength(maxLength: 100)
            .HasConversion(
                state => state.Value,
                value => new State(value))
            .HasColumnName(name: "state");

        builder.Property(address => address.Country)
            .HasMaxLength(maxLength: 100)
            .HasConversion(
                country => country.Value,
                value => new Country(value))
            .HasColumnName(name: "country");

        builder.Property(address => address.PostalCode)
            .HasMaxLength(maxLength: 35)
            .HasConversion(
                postalCode => postalCode.Value,
                value => new PostalCode(value))
            .HasColumnName(name: "postal_code");

        builder.Property(address => address.UserId)
            .HasMaxLength(maxLength: 100)
            .HasColumnName(name: "user_id");

        builder.HasOne<User>(address => address.User)
            .WithMany(user => user.Addresses)
            .HasForeignKey(address => address.UserId);

        builder.HasIndex(address => address.Guid)
            .IsUnique();
    }
}
