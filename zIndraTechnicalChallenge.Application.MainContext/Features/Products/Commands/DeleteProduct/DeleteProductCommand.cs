using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
