using FluentValidation;

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 20);
            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(10, 200);
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .ScalePrecision(2, 10);
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0);
        }
    }
}
