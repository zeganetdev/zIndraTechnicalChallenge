using Microsoft.EntityFrameworkCore;
using zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg;
using zIndraTechnicalChallenge.Infrastructure.Data.Context;
using zIndraTechnicalChallenge.Infrastructure.Data.SeedWork;

namespace zIndraTechnicalChallenge.Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }



    }
}
