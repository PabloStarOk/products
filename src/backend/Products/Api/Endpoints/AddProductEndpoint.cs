using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Products.Application;
using Products.Domain;

namespace Products.Api.Endpoints;

/// <summary>
/// Provides the endpoint for adding a new product.
/// </summary>
public static class AddProductEndpoint
{
    /// <summary>
    /// Maps the POST endpoint for adding a product to the specified endpoint route builder.
    /// </summary>
    /// <param name="builder">The endpoint route builder to which the endpoint will be mapped.</param>
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost(string.Empty, HandleAsync);
    }

    private static async Task<IResult> HandleAsync(
        [FromBody] CreateProductRequest request,
        IProductRepository repository,
        IValidator<CreateProductRequest> validator,
        CancellationToken cancellationToken = default)
    {
        ValidationResult validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Results.ValidationProblem(validation.ToDictionary());
        }

        bool skuUnique = await repository.IsSkuUniqueAsync(request.Sku, cancellationToken);
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
            Name = request.Name,
            Sku = request.Sku,
            Price = request.Price,
            Stock = request.Stock,
            Category = request.Category,
        };
        await repository.AddAsync(product, cancellationToken);
        return Results.CreatedAtRoute(GetProductByIdEndpoint.EndpointName, new { id = product.Id }, product);
    }
}