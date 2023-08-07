using ASP.NET_HW_9.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HW_9.Pages.Product {
    [Authorize(Roles = $"Admin")]
    public class EditModel : PageModel {
        private readonly DataContext _context;

        public EditModel(DataContext context) {
            _context = context;
        }

        [BindProperty]
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
        
        public async Task<IActionResult> OnPostAsync() {
            // if(!User.Identity.IsAuthenticated || User.)

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(Product).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!ProductExists(Product.Id)) {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id) {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}