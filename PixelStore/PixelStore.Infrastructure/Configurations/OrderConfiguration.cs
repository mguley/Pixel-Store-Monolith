using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStore.Domain.Customer;
using PixelStore.Domain.OrderItem;
using OrderType = PixelStore.Domain.Order.Order;

namespace PixelStore.Infrastructure.Configurations;

/// <summary>
/// Configures the entity model for the Order entity.
/// </summary>
public class OrderConfiguration : IEntityTypeConfiguration<OrderType>
{
    /// <summary>
    /// Configures the entity model for the Order entity using the Fluent API.
    /// </summary>
    /// <param name="builder">Provides a simple API for configuring an EntityType.</param>
    public void Configure(EntityTypeBuilder<OrderType> builder)
    {
        builder.ToTable(name: "orders");

        builder.HasKey(order => order.Id);

        builder.Property(order => order.Id)
            .HasColumnName(name: "Id")
            .ValueGeneratedOnAdd();

        builder.Property(order => order.CustomerId)
            .HasMaxLength(maxLength: 100)
            .HasColumnName(name: "customer_id");

        builder.Property(order => order.OrderDate)
            .HasColumnName(name: "order_date");

        builder.HasOne<Customer>(order => order.Customer)
            .WithMany()
            .HasForeignKey(order => order.CustomerId);

        builder.HasMany<OrderItem>(order => order.OrderItems)
            .WithOne(orderItem => orderItem.Order)
            .HasForeignKey(orderItem => orderItem.OrderId);
    }
}
