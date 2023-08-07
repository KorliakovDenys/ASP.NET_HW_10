using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_HW_9.Models;
using ASP.NET_HW_9.Data;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_HW_9.Pages.Cart {
    [Authorize(Roles = "User")]
    public class DeleteModel : PageModel {
        private readonly DataContext _context;

        public DeleteModel(DataContext context) {
            _context = context;
        }

        [BindProperty]
        public CartPosition CartPosition { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null || _context.CartPositions == null) {
                return NotFound();
            }

            var cartposition = await _context.CartPositions.FirstOrDefaultAsync(m => m.Id == id);

            if (cartposition == null) {
                return NotFound();
            }

            CartPosition = cartposition;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null || _context.CartPositions == null) {
                return NotFound();
            }

            var cartposition = await _context.CartPositions.FindAsync(id);

            if (cartposition != null) {
                CartPosition = cartposition;
                _context.CartPositions.Remove(CartPosition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}