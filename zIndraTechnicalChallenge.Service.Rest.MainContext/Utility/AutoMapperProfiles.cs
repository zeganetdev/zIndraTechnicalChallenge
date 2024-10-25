using AutoMapper;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.CreateProduct;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.UpdateProduct;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Queries.GetListProduct;
using zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg;

namespace zIndraTechnicalChallenge.Service.Rest.MainContext.Utility
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, CreateProductResultCommand>().ReverseMap();

            CreateMap<Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductResultCommand>().ReverseMap();

            CreateMap<Product, GetListProductCommand>().ReverseMap();
            CreateMap<Product, GetListProductResultCommand>().ReverseMap();
            CreateMap<IEnumerable<Product>, GetListProductResultCommand>().ReverseMap();
        }
    }
}
