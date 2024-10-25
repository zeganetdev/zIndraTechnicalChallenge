using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using zIndraTechnicalChallenge.Domain.MainContext.SeedWork;

namespace zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg
{
    public class Product : Entity, IAggregateRoot
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } 

        [Required]
        public int Stock { get; set; }

        public void Modify(Product product)
        {
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            Stock = product.Stock;
        }
    }
}
