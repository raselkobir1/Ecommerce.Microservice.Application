using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repository
{
    public class BusketRepository : IBusketRepository
    {
        private readonly IDistributedCache _redishCache;
        public BusketRepository(IDistributedCache redishCache)
        {
            _redishCache = redishCache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redishCache.GetStringAsync(userName);
            if (!string.IsNullOrEmpty(basket))
            {
                return JsonConvert.DeserializeObject<ShoppingCart>(basket);
            }
            else
            {
                return null;
            }
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redishCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
            await _redishCache.RemoveAsync(userName);
        }
    }
}
