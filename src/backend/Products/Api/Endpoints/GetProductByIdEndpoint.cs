using Microsoft.AspNetCore.Mvc;
using Products.Application;
using Products.Domain;

namespace Products.Api.Endpoints;

/// <summary>
/// Provides the endpoint logic for retrieving a product by its ID.
/// </summary>
public static class GetProductByIdEndpoint
{
    /// <summary>
    /// The name of the endpoint for retrieving a product by its ID.
    /// </summary>
    public const string EndpointName = "GetProductById";

    private const string Pattern = "/{id}";

    /// <summary>
    /// Maps the GET endpoint for retrieving a product by its ID.
    /// </summary>
    /// <param name="builder">The endpoint route builder to which the endpoint is mapped.</param>
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet(Pattern, HandleAsync)
            .WithName(EndpointName)
            .WithSummary("Get Product")
            .WithDescription("Get a product by its id.")
            .Produces<Product>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        IProductRepository repository,
        CancellationToken cancellationToken = default)
    {
        Product? product = await repository.GetByIdAsync(id, cancellationToken);
        return product is null
            ? Results.Problem(detail: $"No product found with id '{id}'", statusCode: StatusCodes.Status404NotFound)
            : Results.Ok(product);
    }
}