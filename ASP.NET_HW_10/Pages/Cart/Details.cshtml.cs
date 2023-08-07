using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_HW_9.Models;
using ASP.NET_HW_9.Data;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_HW_9.Pages.Cart {
    [Authorize(Roles = "User")]
    public class DetailsModel : PageModel {
        private readonly DataContext _context;

        public DetailsModel(DataContext context) {
            _context = context;
        }

        public CartPosition CartPosition { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null || _context.CartPositions == null) {
                return NotFound();
            }

            var cartPosition = await _context.CartPositions.FirstOrDefaultAsync(m => m.Id == id);
            if (cartPosition == null) {
                return NotFound();
            }

            CartPosition = cartPosition;

            return Page();
        }
    }
}