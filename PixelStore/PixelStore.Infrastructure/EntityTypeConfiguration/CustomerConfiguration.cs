using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStore.Domain.Customer;
using PixelStore.Domain.Users;
using Email = PixelStore.Domain.Customer.Email;

namespace PixelStore.Infrastructure.EntityTypeConfiguration;

/// <summary>
/// Configures the entity model for the Customer entity.
/// </summary>
internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    /// <summary>
    /// Configures the entity model for the Customer entity using the Fluent API.
    /// </summary>
    /// <param name="builder">Provides a simple API for configuring an EntityType.</param>
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(name: "customers");

        builder.HasKey(customer => customer.Id);

        builder.Property(customer => customer.Id)
            .HasColumnName(name: "Id")
            .ValueGeneratedOnAdd();

        builder.Property(customer => customer.Guid)
            .HasColumnName(name: "guid");

        builder.Property(customer => customer.UserId)
            .HasMaxLength(maxLength: 100)
            .HasColumnName(name: "user_id");
        
        builder.Property(customer => customer.Email)
            .HasMaxLength(maxLength: 200)
            .HasConversion(
                email => email.Value,
                value => new Email(value))
            .HasColumnName(name: "email");

        builder.HasOne<User>(customer => customer.User)
            .WithOne()
            .HasForeignKey<Customer>(customer => customer.UserId);

        builder.HasIndex(customer => customer.Guid)
            .IsUnique();
    }
}
