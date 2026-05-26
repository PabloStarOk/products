using System.ComponentModel.DataAnnotations;

namespace Products.Api.Endpoints;

/// <summary>
/// Request model for creating or updating a product.
/// </summary>
/// <param name="Name">The name of the product.</param>
/// <param name="Sku">The SKU of the product.</param>
/// <param name="Price">The price of the product.</param>
/// <param name="Stock">The available stock quantity for the product.</param>
/// <param name="Category">The category of the product. Optional.</param>
public sealed record ProductRequest(
    string Name,
    string Sku,
    decimal Price,
    int Stock,
    string? Category);