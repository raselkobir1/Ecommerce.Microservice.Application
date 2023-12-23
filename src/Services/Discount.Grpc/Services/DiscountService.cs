using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger _logger;
        public DiscountService(ICouponRepository couponRepository, ILogger logger)
        {
            _couponRepository = couponRepository;
            _logger = logger;   
        }
        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found respected this product."));

            var result = new CouponRequest
            {
                Id = coupon.Id,
                ProductId = coupon.ProductId,
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName,
            };

            _logger.LogInformation($"Discount is retrieve for Product Name:{coupon.ProductName}, ProductId: {coupon.ProductId}, Amount:{coupon.Amount}");
            return result;
        }
    }
}
