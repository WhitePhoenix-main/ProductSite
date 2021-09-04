using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductsSite
{
    public class ProductsSiteContext : DbContext
    {
        public ProductsSiteContext (DbContextOptions<ProductsSiteContext> options)
            : base(options)
        {
        }

        public DbSet<ProductsSite.Product> Product { get; set; }
    }
}
