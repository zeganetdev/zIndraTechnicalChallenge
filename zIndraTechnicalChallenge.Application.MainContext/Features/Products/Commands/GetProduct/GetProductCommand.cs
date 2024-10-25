using MediatR;


namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.GetProduct
{
    public record GetProductCommand : IRequest<GetProductResultCommand>
    {
        public Guid Id { get; set; }
    }
}
