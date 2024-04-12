using Bogus;
using PixelStore.Domain.Abstractions;
using PixelStore.Domain.Products;
using PixelStore.Domain.Products.Price;
using PixelStore.Domain.Users;

namespace PixelStore.WebApi.Extensions;

/// <summary>
/// Provides extension methods for seeding the database with initial data.
/// </summary>
internal static class SeedDataExtensions
{
    private const int DefaultItemsToGenerate = 15;
    
    /// <summary>
    /// Seeds the database with initial data for products and users.
    /// </summary>
    /// <param name="applicationBuilder">The application builder used to access services.</param>
    /// <returns>The original application builder for chaining.</returns>
    public static async Task<IApplicationBuilder> SeedData(this IApplicationBuilder applicationBuilder)
    {
        using IServiceScope scope = applicationBuilder.ApplicationServices.CreateScope();
        IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await GenerateProductsAsync(service: scope.ServiceProvider);
        await GenerateUsersAsync(service: scope.ServiceProvider);

        await unitOfWork.SaveChangesAsync();
        return applicationBuilder;
    }

    /// <summary>
    /// Asynchronously generates and adds products to the database.
    /// </summary>
    /// <param name="service">Service provider to resolve repository services.</param>
    private static async Task GenerateProductsAsync(IServiceProvider service)
    {
        IProductRepository productRepository = service.GetRequiredService<IProductRepository>();
        IEnumerable<Product> products = GenerateProductData(count: DefaultItemsToGenerate);
        foreach (Product product in products)
        {
            await productRepository.AddAsync(entity: product);
        }
    }

    /// <summary>
    /// Generates a list of fake products using the Bogus library.
    /// </summary>
    /// <param name="count">The number of products to generate.</param>
    /// <returns>A list of randomly generated products.</returns>
    private static IEnumerable<Product> GenerateProductData(int count)
    {
        Currency[] currencyList = new[] { Currency.Usd, Currency.Eur };
        Faker<Product> productFaker = new Faker<Product>()
            .RuleFor(product => product.Guid, setter: faker => faker.Random.Guid())
            .RuleFor(product => product.Name, setter: faker => new Name(Value: faker.Commerce.ProductName()))
            .RuleFor(product => product.Price,
                setter: faker =>
                    new Money(Amount: faker.Finance.Amount(5, 100), Currency: faker.PickRandom(currencyList)))
            .RuleFor(product => product.Description,
                setter: faker => new Description(Value: faker.Commerce.ProductDescription()));

        return productFaker.Generate(count: count);
    }

    /// <summary>
    /// Asynchronously generates and adds users to the database.
    /// </summary>
    /// <param name="service">Service provider to resolve repository services.</param>
    private static async Task GenerateUsersAsync(IServiceProvider service)
    {
        IUserRepository userRepository = service.GetRequiredService<IUserRepository>();
        IEnumerable<User> users = GenerateUserData(count: DefaultItemsToGenerate);
        foreach (User user in users)
        {
            await userRepository.AddAsync(entity: user);
        }
    }

    /// <summary>
    /// Generates a list of fake users using the Bogus library.
    /// </summary>
    /// <param name="count">The number of users to generate.</param>
    /// <returns>A list of randomly generated users.</returns>
    private static IEnumerable<User> GenerateUserData(int count)
    {
        Faker<User> userFaker = new Faker<User>()
            .RuleFor(user => user.Guid, setter: faker => faker.Random.Guid())
            .RuleFor(user => user.FirstName, setter: faker => new FirstName(Value: faker.Name.FirstName()))
            .RuleFor(user => user.LastName, setter: faker => new LastName(Value: faker.Name.LastName()))
            .RuleFor(user => user.Email, setter: faker => new Email(Value: faker.Internet.Email()));

        return userFaker.Generate(count: count);
    }
}
