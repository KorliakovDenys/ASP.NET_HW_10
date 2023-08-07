using ASP.NET_HW_9.Data;
using ASP.NET_HW_9.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HW_9.Pages.Product {
    public class DetailsModel : PageModel {
        private readonly DataContext _context;

        private readonly IProductCartService _productCartService;

        public DetailsModel(DataContext context, IProductCartService productCartService) {
            _context = context;
            _productCartService = productCartService;
        }

        public Models.Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null || _context.Products == null) {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) {
                return NotFound();
            }

            Product = product;

            return Page();
        }

        public async Task<IActionResult> OnPostCartAsync(int productId) {
            if (int.TryParse(User.FindFirst("Id")?.Value, out var userId)) {
                await _productCartService.AddToCartAsync(userId, productId);
            }
            return RedirectToPage("./Index");
        }
    }
}