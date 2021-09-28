using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductsSite;

namespace ProductsSite
{
    public class ProductDeleteModel : PageModel
    {
        private readonly ProductsSite.ProductsSiteContext _context;

        private IProductsRepository _productsRepository;

        public ProductDeleteModel(ProductsSite.ProductsSiteContext context, IProductsRepository productsRepository)
        {
            _context = context;
            _productsRepository = productsRepository;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FindAsync(id);

            if (Product != null)
            {
                _context.Product.Remove(Product);
                _productsRepository.DelFolder(_productsRepository.GetFolder(Product));
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Products/ProductIndex");
        }
    }
}
