using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStore.Domain.Products;
using PixelStore.Domain.Products.Price;

namespace PixelStore.Infrastructure.EntityTypeConfiguration;

/// <summary>
/// Configures the entity model for the Product entity.
/// </summary>
internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <summary>
    /// Configures the entity model for the Product entity using the Fluent API.
    /// </summary>
    /// <param name="builder">Provides a simple API for configuring an EntityType.</param>
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(name: "products");
        
        builder.HasKey(product => product.Id);
        
        builder.Property(product => product.Id)
            .HasColumnName(name: "Id")
            .ValueGeneratedOnAdd();

        builder.Property(product => product.Guid)
            .HasColumnName(name: "guid");

        builder.Property(product => product.Name)
            .HasMaxLength(100)
            .HasConversion(
                name => name.Value, 
                value => new Name(value))
            .IsRequired()
            .HasColumnName(name: "name");

        builder.Property(product => product.Description)
            .HasMaxLength(200)
            .HasConversion(description => description.Value,
                value => new Description(value))
            .HasColumnName(name: "description");

        builder.OwnsOne(product => product.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(
                    currency => currency.Code,
                    code => Currency.FromCode(code))
                .HasColumnName(name: "currency_code")
                .HasMaxLength(10);
            priceBuilder.Property(money => money.Amount)
                .HasColumnName(name: "price");
        });

        builder.HasIndex(product => product.Guid)
            .IsUnique();
    }
}
