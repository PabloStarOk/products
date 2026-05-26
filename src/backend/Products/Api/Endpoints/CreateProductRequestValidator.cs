using FluentValidation;

namespace Products.Api.Endpoints;

/// <summary>
/// Validator for <see cref="CreateProductRequest"/>. Ensures that product creation requests meet required validation rules.
/// </summary>
public sealed class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductRequestValidator"/> class.
    /// </summary>
    public CreateProductRequestValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Sku).NotEmpty();
        RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
        RuleFor(p => p.Stock).GreaterThanOrEqualTo(0);
    }
}