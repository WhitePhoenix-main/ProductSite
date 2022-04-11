using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProductsSite
{
    [Authorize]
    public class ProductPictureEditModel : PageModel, IHasProduct
    {
        public ProductPictureEditModel(ProductsSiteContext context, INormalizer normalizer,
            IProductsRepository productsRepository)
        {
            _context = context;
            _normalizer = normalizer;
            _productsRepository = productsRepository;
        }
        private ProductsSiteContext _context { get; init; }
        private INormalizer _normalizer { get; init; }

        private IProductsRepository _productsRepository { get; init; }

        public ProductRecord? ProductRecord { get; set; }
        public bool IsNewRec { get; set; }

        [BindProperty] public string? ProductId { get; set; }
        [BindProperty] public bool OnPreview { get; set; }
        
        [BindProperty] public string? FileName { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            } 
            ProductRecord = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (ProductRecord == null)
            {
                return NotFound();
            }
            ProductId = id;
            return Page();
        }

        public async Task<IActionResult> OnPostSetPreviewAsync()
        {
            //TODO: Вынести в методы
            if (string.IsNullOrWhiteSpace(ProductId))
            {
                return BadRequest();
            }
            
            if (string.IsNullOrWhiteSpace(FileName))
            {
                return BadRequest();
            }

            ProductRecord = await _context.Set<ProductRecord>()
                .FirstOrDefaultAsync(x => x.Id == ProductId);
            if (ProductRecord is null)
            {
                return NotFound();
            }
            
            ProductRecord.PreviewName = FileName;
            await _context.SaveChangesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteImageAsync()
        {
            if (string.IsNullOrWhiteSpace(ProductId))
            {
                return BadRequest();
            }
            if (string.IsNullOrWhiteSpace(FileName))
            {
                return BadRequest();
            }
            ProductRecord = await _context.Set<ProductRecord>()
                .FirstOrDefaultAsync(x => x.Id == ProductId);
            if (ProductRecord is null)
            {
                return NotFound();
            } 
            var s = _productsRepository.GetFolder(ProductRecord);
            if (FileName == ProductRecord.PreviewName)
            {
                ProductRecord.PreviewName = null;
            }
            System.IO.File.Delete($"{s}\\{FileName}");
            await _context.SaveChangesAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //ModelState.AddModelError(nameof(Product.Price), "error message from controller");
            if (string.IsNullOrWhiteSpace(ProductId))
            {
                return BadRequest();
            }

            var productPreview = Request.Form.Files.FirstOrDefault(); 
            
            if (productPreview is null || productPreview.Length == 0)
            {
                return BadRequest();
            }
            ProductRecord = await _context.Set<ProductRecord>()
                .FirstOrDefaultAsync(x => x.Id == ProductId);
            if (ProductRecord is null)
            {
                return NotFound();
            }

            await _productsRepository.SaveFileAsync(ProductRecord, productPreview);
            await _context.SaveChangesAsync();

                return Page();
        }
        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}