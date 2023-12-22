using Basket.API.Models;

namespace Basket.API.Repository
{
    public interface IBusketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName); 
    }
}
