using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_HW_9.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_HW_9.Pages.Cart {
    [Authorize(Roles = "User")]
    public class IndexModel : PageModel {
        private readonly Data.DataContext _context;

        public IndexModel(Data.DataContext context) {
            _context = context;
        }

        public IList<CartPosition> CartPosition { get; set; } = new List<CartPosition>();

        public async Task OnGetAsync() {
            if (_context.CartPositions != null) {
                if (int.TryParse(User.FindFirst("Id")?.Value, out var id)) {
                    CartPosition = await _context.CartPositions.Where(cp => cp.UserId == id)
                        .Include(c => c.Product).ToListAsync();
                }
            }
        }
    }
}