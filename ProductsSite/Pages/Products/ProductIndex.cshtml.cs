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
        public  ProductsSite.ProductsSiteContext Context { get; }

        public ProductIndexModel(ProductsSite.ProductsSiteContext context)
        {
            Context = context;
        }
        
        public IList<ProductRecord> Product { get;set; }

        public async Task OnGetAsync(string? search, string? productType)
        {
            ViewData["search"] = search;
            ViewData["productType"] = productType;
            //TODO: Пересмотреть логику
            var query = Context.Product.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search) && string.IsNullOrWhiteSpace(productType))
                query = query
                    .Where(product => product.CategoryId == search 
                    || product.ProductName == search);
            else if (!string.IsNullOrWhiteSpace(productType) && !string.IsNullOrWhiteSpace(search))
                query = query
                    .Where(product => product.CategoryId == productType
                                      || product.ProductName == search);
            else if (!string.IsNullOrWhiteSpace(productType) )
                query = query
                    .Where(product => product.CategoryId == productType);
            
            Product = await query
                .OrderBy(product => product.CategoryId)
                .ThenBy(product => product.ProductName)
                .ToListAsync();
        }
        
    }
}
