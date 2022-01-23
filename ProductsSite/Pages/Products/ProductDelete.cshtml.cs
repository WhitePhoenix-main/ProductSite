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
        public ProductRecord ProductRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductRecord = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (ProductRecord == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductRecord = await _context.Product.FindAsync(id);

            if (ProductRecord != null)
            {
                _context.Product.Remove(ProductRecord);
                _productsRepository.DelFolderWithFiles(_productsRepository.GetFolder(ProductRecord));
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Products/ProductIndex");
        }
    }
}
