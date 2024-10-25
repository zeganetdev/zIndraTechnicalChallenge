using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zIndraTechnicalChallenge.Application.MainContext.SeedWork
{
    public class BaseCommandPaginatorResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
