using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductsSite;

namespace ProductsSite
{
    public class ProductEditModel : PageModel
    {
        private readonly ProductsSite.ProductsSiteContext _context;

        public ProductEditModel(ProductsSite.ProductsSiteContext context)
        {
            _context = context;
        }

        [BindProperty] public Product Product { get; set; }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //ModelState.AddModelError(nameof(Product.Price), "error message from controller");
            if (Product.PriceInput != null)
            {
                Normalaizer norm = new Normalaizer(Product.PriceInput);
                if (norm.GetNormStrRu() == -1)
                {
                    ModelState.AddModelError(nameof(Product.Price), "error during input");
                }
                else
                {
                    Product.Price = norm.GetNormStrRu();
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Products/ProductIndex");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}