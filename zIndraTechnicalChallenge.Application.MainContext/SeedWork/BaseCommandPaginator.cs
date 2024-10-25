using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zIndraTechnicalChallenge.Application.MainContext.SeedWork
{
    public record BaseCommandPaginator
    {
        public int PageIndex { get; set; } = 0;
        public int PageCount { get; set; } = 10;
    }
}
