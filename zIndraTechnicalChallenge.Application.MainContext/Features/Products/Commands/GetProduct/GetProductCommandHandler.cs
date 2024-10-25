using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.CreateProduct;
using zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg;
using zIndraTechnicalChallenge.Domain.MainContext.SeedWork;

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.GetProduct
{
    public class GetProductCommandHandler : IRequestHandler<GetProductCommand, GetProductResultCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductCommandHandler> _logger;

        public GetProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetProductCommandHandler> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GetProductResultCommand> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.FindAsync(request.Id, cancellationToken);
            if (product == null) throw new ApplicationException("El producto no existe");
            return _mapper.Map<GetProductResultCommand>(product);
        }

    }
}
