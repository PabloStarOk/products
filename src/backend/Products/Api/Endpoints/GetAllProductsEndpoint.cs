using Products.Application;
using Products.Domain;

namespace Products.Api.Endpoints;

/// <summary>
/// Provides the endpoint for retrieving all products.
/// </summary>
public static class GetAllProductsEndpoint
{
    /// <summary>
    /// Maps the GET endpoint for retrieving all products to the specified endpoint route builder.
    /// </summary>
    /// <param name="builder">The endpoint route builder to which the endpoint will be mapped.</param>
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet(string.Empty, HandleAsync);
    }

    private static async Task<IResult> HandleAsync(
        IProductRepository repository,
        CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Product> products = await repository.GetAllAsync(cancellationToken);
        return products.Count is 0
            ? Results.Problem(detail: "No products found", statusCode: StatusCodes.Status404NotFound)
            : Results.Ok(products);
    }
}