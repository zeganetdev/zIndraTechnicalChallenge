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

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResultCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<UpdateProductCommandHandler> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UpdateProductResultCommand> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("init UpdateProductCommandHandler.Handle");

            var product = await _unitOfWork.ProductRepository.FindAsync(request.Id, cancellationToken);

            if (product == null) throw new ApplicationException("El producto no existe");

            var productModify = _mapper.Map<Product>(request);

            product.Modify(productModify);

            using (await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                _unitOfWork.ProductRepository.Update(product);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            };
            
            return new UpdateProductResultCommand { Id = product.Id };
        }

    }
}
