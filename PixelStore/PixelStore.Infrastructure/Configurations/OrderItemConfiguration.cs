using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStore.Domain.Order;
using PixelStore.Domain.OrderItem;
using PixelStore.Domain.Products;
using OrderItemType = PixelStore.Domain.OrderItem.OrderItem;

namespace PixelStore.Infrastructure.Configurations;

/// <summary>
/// Configures the entity model for the Order item entity.
/// </summary>
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemType>
{
    /// <summary>
    /// Configures the entity model for the Order Item entity using the Fluent API.
    /// </summary>
    /// <param name="builder">Provides a simple API for configuring an EntityType.</param>
    public void Configure(EntityTypeBuilder<OrderItemType> builder)
    {
        builder.ToTable(name: "order_items");

        builder.HasKey(orderItem => orderItem.Id);

        builder.Property(orderItem => orderItem.Id)
            .HasColumnName(name: "Id")
            .ValueGeneratedOnAdd();

        builder.Property(orderItem => orderItem.Guid)
            .HasColumnName(name: "guid");

        builder.Property(orderItem => orderItem.OrderId)
            .HasMaxLength(maxLength: 100)
            .HasColumnName(name: "order_id");

        builder.Property(orderItem => orderItem.ProductId)
            .HasMaxLength(maxLength: 100)
            .HasColumnName(name: "product_id");

        builder.Property(orderItem => orderItem.Quantity)
            .HasMaxLength(maxLength: 100)
            .HasConversion(
                quantity => quantity.Value,
                value => new Quantity(value))
            .HasColumnName(name: "quantity");

        builder.Property(orderItem => orderItem.UnitPrice)
            .HasMaxLength(maxLength: 100)
            .HasConversion(
                unitPrice => unitPrice.Value,
                value => new UnitPrice(value))
            .HasColumnName(name: "unit_price");

        builder.HasOne<Order>(orderItem => orderItem.Order)
            .WithMany(order => order.OrderItems)
            .HasForeignKey(orderItem => orderItem.OrderId);

        builder.HasOne<Product>(orderItem => orderItem.Product)
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId);
    }
}
