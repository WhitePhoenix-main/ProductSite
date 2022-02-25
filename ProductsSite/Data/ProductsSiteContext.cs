using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductsSite.Pages.Wishlist.Data;

namespace ProductsSite
{
    public class ProductsSiteContext : IdentityDbContext<SiteUsers>
    {
        public ProductsSiteContext (DbContextOptions<ProductsSiteContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<WishlistRecord>()
                .HasKey(x => new { x.UserId, x.ProductId });
        }

        public DbSet<ProductRecord> Product { get; set; }
        
        public DbSet<CategoryRecord> Categories {get; set; }
        
        // Таблица для заказов
        
        // Таблица Категорий
    }
}
