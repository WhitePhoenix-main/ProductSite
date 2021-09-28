using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsSite;

namespace ProductsSite
{
    public class ProductCreateModel : PageModel
    {
        private readonly ProductsSite.ProductsSiteContext _context;

        public ProductCreateModel(ProductsSite.ProductsSiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Product.Add(Product);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Products/ProductIndex");
        }
    }
}
