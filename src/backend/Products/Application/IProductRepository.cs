using Products.Domain;

namespace Products.Application;

/// <summary>
/// Defines the contract for a repository that manages <see cref="Product"/> entities.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Retrieves all products asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A read-only list of all products.</returns>
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The product if found; otherwise, null.</returns>
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new product asynchronously.
    /// </summary>
    /// <param name="product">The product to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product asynchronously.
    /// </summary>
    /// <param name="product">The product to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the provided SKU is unique among all products asynchronously.
    /// </summary>
    /// <param name="sku">The SKU to check for uniqueness.</param>
    /// <param name="excludeId">The ID of a product to exclude from the uniqueness check (useful when updating a product).</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if the SKU is unique; otherwise, false.</returns>
    Task<bool> IsSkuUniqueAsync(string sku, int? excludeId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a product with the specified unique identifier exists asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the product to check for existence.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if the product exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if the product was deleted; otherwise, false.</returns>
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}