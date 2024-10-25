using FluentValidation;

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.GetProduct
{
    public class GetProductCommandValidator : AbstractValidator<GetProductCommand>
    {
        public GetProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
