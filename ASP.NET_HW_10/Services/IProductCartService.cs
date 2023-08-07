namespace ASP.NET_HW_9.Services {
    public interface IProductCartService {
        public Task AddToCartAsync(int userId, int productId);
    }
}