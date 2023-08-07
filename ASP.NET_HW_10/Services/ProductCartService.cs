using ASP.NET_HW_9.Data;
using ASP.NET_HW_9.Models;

namespace ASP.NET_HW_9.Services {
    public class ProductCartService : IProductCartService {
        private readonly DataContext _context;

        public ProductCartService(DataContext context) {
            _context = context;
        }
        public async Task AddToCartAsync(int userId, int productId) {
            var cartPosition = _context.CartPositions!.FirstOrDefault(cp => cp.ProductId == productId && cp.UserId == userId);
                
            if (cartPosition != null) {
                cartPosition.Amount++;
            }
            else {
                cartPosition = new CartPosition { Amount = 1, ProductId = productId, UserId = userId };
                _context.CartPositions?.Add(cartPosition);
            }

            await _context.SaveChangesAsync();
        }
    }
}