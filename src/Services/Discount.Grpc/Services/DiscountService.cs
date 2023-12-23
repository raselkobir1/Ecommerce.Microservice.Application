using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public DiscountService(ICouponRepository couponRepository, ILogger logger, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found respected this product."));
            #region Manual mapping
            //var result = new CouponRequest
            //{
            //    Id = coupon.Id,
            //    ProductId = coupon.ProductId,
            //    Amount = coupon.Amount,
            //    Description = coupon.Description,
            //    ProductName = coupon.ProductName,
            //};
            #endregion
            _logger.LogInformation($"Discount is retrieve for Product Name:{coupon.ProductName}, ProductId: {coupon.ProductId}, Amount:{coupon.Amount}");
            var couponRequest = _mapper.Map<CouponRequest>(coupon);
            return couponRequest;
        }

        public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
        {
            
            var coupon = _mapper.Map<Coupon>(request);
            bool isSaved = await _couponRepository.CreateDiscount(coupon);
            if (isSaved)
                _logger.LogInformation($"Discount created successfully with ProductName:{coupon.ProductName}, ProductId: {coupon.ProductId}, Amount:{coupon.Amount}");
            _logger.LogWarning("Discount created failed");

            var response = _mapper.Map<CouponRequest>(coupon);
            return response;
        }
        public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool isUpdated = await _couponRepository.UpdateDiscount(coupon);
            if (isUpdated)
                _logger.LogInformation($"Discount Updated successfully with ProductName:{coupon.ProductName}, ProductId: {coupon.ProductId}, Amount:{coupon.Amount}");
            _logger.LogWarning("Discount Updated failed");

            var response = _mapper.Map<CouponRequest>(coupon);
            return response;
        }

        public override async Task<DeleteCouponResponse> DeleteDiscount(DeleteCouponRequest request, ServerCallContext context)
        {
            bool isDeleted = await _couponRepository.DeleteDiscount(request.ProductId);
            if (isDeleted)
                _logger.LogInformation($"Discount delete successfully done with productId:{request.ProductId}");
            _logger.LogWarning("Discount delete failed");

            return new DeleteCouponResponse { Success = isDeleted };
        }
    }
}
