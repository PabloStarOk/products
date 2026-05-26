using FluentValidation;

namespace Products.Api.Endpoints;

/// <summary>
/// Validator for <see cref="ProductRequest"/>. Ensures that product requests meet required validation rules.
/// </summary>
public sealed class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRequestValidator"/> class.
    /// </summary>
    public ProductRequestValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Sku).NotEmpty();
        RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
        RuleFor(p => p.Stock).GreaterThanOrEqualTo(0);
    }
}