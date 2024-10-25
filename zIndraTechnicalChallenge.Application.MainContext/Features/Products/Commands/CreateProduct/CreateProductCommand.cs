using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<CreateProductResultCommand>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
