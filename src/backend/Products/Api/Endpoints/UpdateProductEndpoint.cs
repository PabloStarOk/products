using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Products.Application;
using Products.Domain;

namespace Products.Api.Endpoints;

/// <summary>
/// Provides the endpoint for updating a product.
/// </summary>
public static class UpdateProductEndpoint
{
    private const string Pattern = "/{id}";

    /// <summary>
    /// Maps the PUT endpoint for updating a product to the specified endpoint route builder.
    /// </summary>
    /// <param name="builder">The endpoint route builder to which the endpoint will be mapped.</param>
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPut(Pattern, HandleAsync);
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        [FromBody] ProductRequest request,
        IProductRepository repository,
        IValidator<ProductRequest> validator,
        CancellationToken cancellationToken = default)
    {
        ValidationResult validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Results.ValidationProblem(validation.ToDictionary());
        }

        bool exists = await repository.ExistsAsync(id, cancellationToken);
        if (!exists)
        {
            return Results.Problem(
                detail: $"No product found with id '{id}'",
                statusCode: StatusCodes.Status404NotFound);
        }

        bool skuUnique = await repository.IsSkuUniqueAsync(request.Sku, excludeId: id, cancellationToken);
        if (!skuUnique)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "Sku", ["SKU must be unique."] },
            };
            return Results.ValidationProblem(errors);
        }

        var product = new Product
        {
            Id = id,
            Name = request.Name,
            Sku = request.Sku,
            Price = request.Price,
            Stock = request.Stock,
            Category = request.Category,
        };
        await repository.UpdateAsync(product, cancellationToken);
        return Results.Ok(product);
    }
}