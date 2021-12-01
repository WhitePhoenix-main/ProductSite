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
    public class ProductCreateModel : PageModel, IHasProduct
    {
        private readonly ProductsSite.ProductsSiteContext _context;

        public ProductCreateModel(ProductsSite.ProductsSiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Product = new Product() { Id = Guid.NewGuid().ToString() };
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public bool IsNewRec { get; set; } = true;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!String.IsNullOrWhiteSpace(Product.ProductTypeNew))
            {
                Product.ProductType = Product.ProductTypeNew;
            }
            _context.Product.Add(Product);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Products/ProductIndex");
        }
    }
}
