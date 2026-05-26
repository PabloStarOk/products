namespace Products.Domain;

/// <summary>
/// Represents a product.
/// </summary>
public sealed class Product
{
    /// <summary>
    /// Gets the ID of the product.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the name of the product.
    /// </summary>
    required public string Name { get; init; }

    /// <summary>
    /// Gets the SKU of the product.
    /// </summary>
    required public string Sku { get; init; }

    /// <summary>
    /// Gets the price of the product.
    /// </summary>
    public decimal Price { get; init; }

    /// <summary>
    /// Gets the available stock of the product.
    /// </summary>
    public int Stock { get; init; }

    /// <summary>
    /// Gets the category of the product.
    /// </summary>
    public string? Category { get; init; }
}