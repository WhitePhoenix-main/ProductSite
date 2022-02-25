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
        private ProductsSiteContext _context { get; }

        public ProductCreateModel(ProductsSiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ProductRecord = new ProductRecord() { Id = Guid.NewGuid().ToString() };
            return Page();
        }

        [BindProperty]
        public ProductRecord ProductRecord { get; set; }

        public bool IsNewRec { get; set; } = true;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!String.IsNullOrWhiteSpace(ProductRecord.ProductTypeNew))
            {
                ProductRecord.CategoryId = ProductRecord.ProductTypeNew;
            }
            _context.Product.Add(ProductRecord);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Products/ProductIndex");
        }
    }
}
