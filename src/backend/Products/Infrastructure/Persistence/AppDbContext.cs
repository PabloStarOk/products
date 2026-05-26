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

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSeeding(AddSeedData)
            .UseAsyncSeeding(AddSeedDataAsync);
    }

    private static void AddSeedData(DbContext context, bool _)
    {
        bool hasProducts = context.Set<Product>().Any();
        if (hasProducts)
        {
            return;
        }

        context.Set<Product>().AddRange(
            new Product
            {
                Name = "Chaqueta Denim Slim Fit",
                Sku = "TEX-CHQ-001",
                Price = 149900.00m,
                Stock = 45,
                Category = "Chaquetas",
            },
            new Product
            {
                Name = "Camiseta Básica Algodón Regular",
                Sku = "TEX-CAM-002",
                Price = 39900.00m,
                Stock = 120,
                Category = "Camisetas",
            },
            new Product
            {
                Name = "Pantalón Jean Jogger Confort",
                Sku = "TEX-JNS-003",
                Price = 119900.00m,
                Stock = 60,
                Category = "Jeans",
            },
            new Product
            {
                Name = "Buso Hoodie Unisex Con Capota",
                Sku = "TEX-BUS-004",
                Price = 89900.00m,
                Stock = 35,
                Category = "Busos",
            },
            new Product
            {
                Name = "Correa Casual Cuero Sintético",
                Sku = "TEX-ACC-005",
                Price = 29900.00m,
                Stock = 80,
                Category = "Accesorios",
            });

        context.SaveChanges();
    }

    private static async Task AddSeedDataAsync(DbContext context, bool _, CancellationToken cancellationToken)
    {
        bool hasProducts = context.Set<Product>().Any();
        if (hasProducts)
        {
            return;
        }

        context.Set<Product>().AddRange(
            new Product
            {
                Name = "Chaqueta Denim Slim Fit",
                Sku = "TEX-CHQ-001",
                Price = 149900.00m,
                Stock = 45,
                Category = "Chaquetas",
            },
            new Product
            {
                Name = "Camiseta Básica Algodón Regular",
                Sku = "TEX-CAM-002",
                Price = 39900.00m,
                Stock = 120,
                Category = "Camisetas",
            },
            new Product
            {
                Name = "Pantalón Jean Jogger Confort",
                Sku = "TEX-JNS-003",
                Price = 119900.00m,
                Stock = 60,
                Category = "Jeans",
            },
            new Product
            {
                Name = "Buso Hoodie Unisex Con Capota",
                Sku = "TEX-BUS-004",
                Price = 89900.00m,
                Stock = 35,
                Category = "Busos",
            },
            new Product
            {
                Name = "Correa Casual Cuero Sintético",
                Sku = "TEX-ACC-005",
                Price = 29900.00m,
                Stock = 80,
                Category = "Accesorios",
            });

        await context.SaveChangesAsync(cancellationToken);
    }
}