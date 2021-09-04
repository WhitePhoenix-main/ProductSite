using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductsSite;

namespace ProductsSite
{
    public class ProductIndexModel : PageModel
    {
        private readonly ProductsSite.ProductsSiteContext _context;

        public ProductIndexModel(ProductsSite.ProductsSiteContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}
