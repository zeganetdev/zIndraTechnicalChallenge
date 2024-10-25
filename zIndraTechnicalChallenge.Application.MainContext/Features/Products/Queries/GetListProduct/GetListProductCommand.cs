using MediatR;
using zIndraTechnicalChallenge.Application.MainContext.SeedWork;

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Queries.GetListProduct
{
    public record GetListProductCommand : BaseCommandPaginator, IRequest<BaseCommandPaginatorResult<GetListProductResultCommand>>
    {
        public string Name { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set; }
    }
}
