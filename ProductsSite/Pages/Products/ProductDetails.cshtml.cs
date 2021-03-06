using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductsSite;

namespace ProductsSite
{
    [Authorize]
    public class ProductDetailsModel : PageModel
    {
        private readonly ProductsSite.ProductsSiteContext _context;

        public ProductDetailsModel(ProductsSite.ProductsSiteContext context)
        {
            _context = context;
        }

        public ProductRecord ProductRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            // TODO: Проверка на пустую строку
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
    }
}
