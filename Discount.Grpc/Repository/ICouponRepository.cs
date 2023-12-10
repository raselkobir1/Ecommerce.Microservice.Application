using Discount.Grpc.Models;

namespace Discount.Grpc.Repository 
{
    public interface ICouponRepository
    {
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productId); 
        Task<Coupon> GetDiscount(string productId);
    }
}
