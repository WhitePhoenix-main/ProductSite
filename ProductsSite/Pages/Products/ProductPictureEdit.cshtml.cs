using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductsSite;

namespace ProductsSite
{
    [Authorize]
    public class ProductPictureEditModel : PageModel, IHasProduct
    {
        private ProductsSite.ProductsSiteContext _context { get; init; }
        private INormalizer _normalizer { get; init; }

        private IProductsRepository _productsRepository { get; init; }

        public ProductPictureEditModel(ProductsSite.ProductsSiteContext context, INormalizer normalizer,
            IProductsRepository productsRepository)
        {
            _context = context;
            _normalizer = normalizer;
            _productsRepository = productsRepository;
        }

        [BindProperty] public ProductRecord ProductRecord { get; set; }
        public bool IsNewRec { get; set; } = false;

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //ModelState.AddModelError(nameof(Product.Price), "error message from controller");

            if (ProductRecord == null)
            {
                ModelState.AddModelError(nameof(ProductRecord), "error during input");
                return Page();
            }

            if (!String.IsNullOrWhiteSpace(ProductRecord.ProductTypeNew))
            {
                ProductRecord.CategoryId = ProductRecord.ProductTypeNew;
            }

            if (ProductRecord.PriceInput != null)
            {
                var norm = _normalizer.GetNormStrRu(ProductRecord.PriceInput);

                if (norm == -1)
                {
                    ModelState.AddModelError(nameof(ProductRecord.Price), "error during input");
                }
                else
                {
                    ProductRecord.Price = norm;
                }
            }

            var file = Request.Form.Files.FirstOrDefault();

            if (file is not null && file.Length > 0)
            {
                var result = await _productsRepository.SaveFileAsync(ProductRecord, file);
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oldProduct = await _context.Set<ProductRecord>()
                .FirstOrDefaultAsync(x => x.Id == ProductRecord.Id);
            if (oldProduct is null)
            {
                return NotFound();
            }
            Console.WriteLine(!String.IsNullOrWhiteSpace(ProductRecord.ProductName));
            Console.WriteLine(!String.IsNullOrWhiteSpace("ProductRecord.ProductName"));
            if (!String.IsNullOrWhiteSpace(ProductRecord.ProductName))
            {
                oldProduct.ProductName = ProductRecord.ProductName;
            }
            _context.Update(oldProduct);
            await _context.SaveChangesAsync();

            Console.WriteLine(ProductRecord.OnPreview);

            return RedirectToPage("/Products/ProductIndex");
        }

        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}