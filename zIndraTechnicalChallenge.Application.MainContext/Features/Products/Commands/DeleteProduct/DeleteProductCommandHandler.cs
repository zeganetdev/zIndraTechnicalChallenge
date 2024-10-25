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

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        async Task<Unit> IRequestHandler<DeleteProductCommand, Unit>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("init DeleteProductCommandHandler.Handle");

            var product = await _unitOfWork.ProductRepository.FindAsync(request.Id, cancellationToken);

            if (product == null) throw new ApplicationException("El producto no existe");

            _unitOfWork.ProductRepository.Remove(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
