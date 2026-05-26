using Microsoft.EntityFrameworkCore;
using Products.Domain;

namespace Products.Infrastructure.Persistence;

/// <summary>
/// The application DB context.
/// </summary>
public sealed class AppDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the Products table in the database.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.Sku).IsUnique();
            entity.Property(p => p.Name).IsRequired();
            entity.Property(p => p.Sku).IsRequired();
            entity.ToTable(t => t.HasCheckConstraint("CK_Product_Price", "Price >= 0"));
            entity.ToTable(t => t.HasCheckConstraint("CK_Product_Stock", "Stock >= 0"));
        });

        base.OnModelCreating(modelBuilder);
    }
}