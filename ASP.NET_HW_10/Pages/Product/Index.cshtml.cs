using ASP.NET_HW_9.Data;
using ASP.NET_HW_9.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HW_9.Pages.Product {
    public class IndexModel : PageModel {
        private readonly DataContext _context;

        private readonly IProductCartService _productCartService;

        public IndexModel(DataContext context, IProductCartService productCartService) {
            _context = context;
            _productCartService = productCartService;
        }

        public IList<Models.Product> Product { get; set; } = new List<Models.Product>();

        public async Task OnGetAsync() {
            if (_context.Products != null) {
                Product = await _context.Products.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostCartAsync(int productId) {
            if (int.TryParse(User.FindFirst("Id")?.Value, out var userId)) {
                await _productCartService.AddToCartAsync(userId, productId);
            }
            
            return RedirectToPage("./Index");
        }
    }
}