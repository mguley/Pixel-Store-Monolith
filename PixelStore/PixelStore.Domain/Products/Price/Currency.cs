using PixelStore.Domain.Exceptions;

namespace PixelStore.Domain.Products.Price;

/// <summary>
/// Represents a specific currency, identified by its ISO currency code.
/// </summary>
public sealed record Currency
{
    internal static readonly Currency None = new(code: "");
    
    public static readonly Currency Usd = new(code: "USD");
    public static readonly Currency Eur = new(code: "EUR");

    /// <summary>
    /// Initializes a new instance of the Currency class.
    /// </summary>
    /// <param name="code">The ISO code representing the currency.</param>
    private Currency(string code) => Code = code;
    
    /// <summary>
    /// Gets the ISO currency code.
    /// </summary>
    public string Code { get; init; }

    /// <summary>
    /// Creates a Currency object based on an ISO currency code.
    /// </summary>
    /// <param name="code">The ISO currency code.</param>
    /// <returns>A Currency object corresponding to the provided ISO code.</returns>
    /// <exception cref="InvalidCurrencyException">Thrown if the provided code does not match any known currency.</exception>
    public static Currency FromCode(string code)
    {
        return AvailableListOfCurrencies.FirstOrDefault(currency => currency.Code == code) ??
               throw new InvalidCurrencyException(message: "Provided currency code is invalid.");
    }
    
    /// <summary>
    /// Lists all available currencies in the system.
    /// </summary>
    public static readonly IReadOnlyCollection<Currency> AvailableListOfCurrencies = new[]
    {
        Usd
    };
}
