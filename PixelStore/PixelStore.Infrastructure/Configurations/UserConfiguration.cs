using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStore.Domain.Users;

namespace PixelStore.Infrastructure.Configurations;

/// <summary>
/// Configures the entity model for the User entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the entity model for the User entity using the Fluent API.
    /// </summary>
    /// <param name="builder">Provides a simple API for configuring an EntityType.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(name: "users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasColumnName(name: "Id")
            .ValueGeneratedOnAdd();

        builder.Property(user => user.Guid)
            .HasColumnName(name: "guid");

        builder.Property(user => user.FirstName)
            .HasMaxLength(maxLength: 100)
            .HasConversion(
                firstName => firstName.Value,
                value => new FirstName(value))
            .HasColumnName(name: "first_name");

        builder.Property(user => user.LastName)
            .HasMaxLength(maxLength: 100)
            .HasConversion(
                lastName => lastName.Value,
                value => new LastName(value))
            .HasColumnName(name: "last_name");

        builder.Property(user => user.Email)
            .HasMaxLength(maxLength: 100)
            .HasConversion(
                email => email.Value,
                value => new Email(value))
            .HasColumnName(name: "email");

        builder.HasIndex(user => user.Email)
            .IsUnique();
        builder.HasIndex(user => user.Guid)
            .IsUnique();
    }
}
