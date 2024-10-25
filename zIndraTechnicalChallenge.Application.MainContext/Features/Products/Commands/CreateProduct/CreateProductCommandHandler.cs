using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg;
using zIndraTechnicalChallenge.Domain.MainContext.SeedWork;

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResultCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProductCommandHandler> _logger;


        public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CreateProductCommandHandler> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateProductResultCommand> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("init CreateProductCommandHandler.Handle");

            var productExists = _unitOfWork.ProductRepository.Where(x => x.Name.Contains(request.Name)).FirstOrDefault();

            if (productExists != null) throw new ApplicationException("El nombre del producto ya existe");

            var product = _mapper.Map<Product>(request);

            using (await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                await _unitOfWork.ProductRepository.AddAsync(product, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            };

            return new CreateProductResultCommand { Id = product.Id };
        }

    }
}
