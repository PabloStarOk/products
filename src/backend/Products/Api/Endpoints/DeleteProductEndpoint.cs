using Microsoft.AspNetCore.Mvc;
using Products.Application;

namespace Products.Api.Endpoints;

/// <summary>
/// Provides the endpoint for deleting a product.
/// </summary>
public static class DeleteProductEndpoint
{
    private const string Pattern = "/{id}";

    /// <summary>
    /// Maps the DELETE endpoint for deleting a product by its ID.
    /// </summary>
    /// <param name="builder">The endpoint route builder to which the endpoint is added.</param>
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapDelete(Pattern, HandleAsync)
            .WithSummary("Delete Product")
            .WithDescription("Delete an existing product from the database.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> HandleAsync(
        [FromRoute] int id,
        IProductRepository repository,
        CancellationToken cancellationToken = default)
    {
        bool exists = await repository.ExistsAsync(id, cancellationToken);
        if (!exists)
        {
            return Results.Problem(
                detail: $"No product found with id '{id}'",
                statusCode: StatusCodes.Status404NotFound);
        }

        await repository.DeleteAsync(id, cancellationToken);
        return Results.NoContent();
    }
}