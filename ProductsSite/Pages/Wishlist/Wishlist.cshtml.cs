using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductsSite.Pages.Wishlist.Data;

namespace ProductsSite
{
    [Authorize]
    public class WishlistModel : PageModel
    {
        public WishlistModel(ProductsSiteContext context, UserManager<SiteUsers> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private ProductsSiteContext _context { get; }
        private UserManager<SiteUsers> _userManager { get; }

        public List<ProductRecord>? Products { get; set; }
        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var query = _context.Product.AsNoTracking()
                .Where(x => _context.Set<WishlistRecord>()
                    .Where(w => w.UserId == userId)
                    .Select(w => w.ProductId)
                    .Contains(x.Id)
                );
            
            Products = await query
                .OrderBy(product => product.CategoryId)
                .ThenBy(product => product.ProductName)
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostAddAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Не указан товар");
            var userId = _userManager.GetUserId(User);
            var record = new WishlistRecord
            {
                ProductId = id,
                UserId = userId
            };
            _context.Set<WishlistRecord>().Add(record);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Products/ProductIndex");
        }
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Не указан товар");
            var userId = _userManager.GetUserId(User);
            var record = await _context.Set<WishlistRecord>()
                .FirstOrDefaultAsync(r => r.ProductId == id && r.UserId == userId);
            
            if (record is null)
            {
                return new OkResult();
            }
            _context.Set<WishlistRecord>().Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Products/ProductIndex");
        }
    }
}