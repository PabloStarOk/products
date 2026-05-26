namespace Products.Api.Endpoints;

/// <summary>
/// Provides a group of endpoints related to product operations.
/// </summary>
public static class ProductEndpointsGroup
{
    private const string GroupPattern = "/api/products";
    private const string GroupTag = "Products";

    /// <summary>
    /// Maps the product-related endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="builder">The endpoint route builder to which the product endpoints will be mapped.</param>
    public static void Map(IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup(GroupPattern).WithTags(GroupTag);
        GetAllProductsEndpoint.Map(group);
        GetProductByIdEndpoint.Map(group);
        AddProductEndpoint.Map(group);
        UpdateProductEndpoint.Map(group);
    }
}