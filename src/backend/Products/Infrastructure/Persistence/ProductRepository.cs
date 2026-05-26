using Microsoft.EntityFrameworkCore;
using Products.Application;
using Products.Domain;

namespace Products.Infrastructure.Persistence;

/// <summary>
/// Repository implementation for managing <see cref="Product"/> entities in the application's database.
/// </summary>
public sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context.</param>
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products
            .Where(p => p.Id == product.Id)
            .ExecuteUpdateAsync(
                builder =>
                {
                    builder.SetProperty(x => x.Name, product.Name);
                    builder.SetProperty(x => x.Sku, product.Sku);
                    builder.SetProperty(x => x.Price, product.Price);
                    builder.SetProperty(x => x.Stock, product.Stock);
                    builder.SetProperty(x => x.Category, product.Category);
                },
                cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> IsSkuUniqueAsync(
        string sku,
        int? excludeId = null,
        CancellationToken cancellationToken = default)
    {
        return !await _context.Products.AnyAsync(p => p.Sku == sku && p.Id != excludeId, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.AnyAsync(p => p.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _context.Products.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);
    }
}