using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsSite
{
    public class ProductsSiteContext : IdentityDbContext<SiteUsers>
    {
        public ProductsSiteContext (DbContextOptions<ProductsSiteContext> options)
            : base(options)
        {
        }

        public DbSet<ProductsSite.Product> Product { get; set; }
        // Таблица для заказов
        // Таблица Категорий
    }
}
