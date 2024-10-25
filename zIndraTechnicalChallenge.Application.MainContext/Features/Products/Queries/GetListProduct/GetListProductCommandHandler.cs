using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.CreateProduct;
using zIndraTechnicalChallenge.Application.MainContext.SeedWork;
using zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg;
using zIndraTechnicalChallenge.Domain.MainContext.SeedWork;

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Queries.GetListProduct
{
    public class GetListProductCommandHandler : IRequestHandler<GetListProductCommand, BaseCommandPaginatorResult<GetListProductResultCommand>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetListProductCommandHandler> _logger;

        public GetListProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetListProductCommandHandler> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseCommandPaginatorResult<GetListProductResultCommand>> Handle(GetListProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("init GetListProductResultCommand.Handle");

            Expression<Func<Product, bool>> whereExpression = product => (string.IsNullOrEmpty(request.Name) || product.Name.Contains(request.Name)) &&
                                                                         ((request.StockMin == 0 && request.StockMax == 0) || product.Stock >= request.StockMin && product.Stock <= request.StockMax);

            Expression<Func<Product, DateTime>> orderByExpression = product => product.RegisterDate;

            bool ascending = false;

            var countProducts = _unitOfWork.ProductRepository.Where(whereExpression).Count();

            var pagedProduts = await _unitOfWork.ProductRepository.GetPagedAsync(request.PageIndex, request.PageCount, whereExpression, orderByExpression, ascending);

            return new BaseCommandPaginatorResult<GetListProductResultCommand>()
            {
                TotalRows = countProducts,
                Data = _mapper.Map<IEnumerable<GetListProductResultCommand>>(pagedProduts)
            };
        }


    }
}