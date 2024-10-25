using FluentValidation;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.UpdateProduct;

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
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
