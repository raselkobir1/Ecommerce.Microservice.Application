using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _grpcDiscountServiceClient;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient grpcClient)
        {
            _grpcDiscountServiceClient = grpcClient;
        }
        public async Task<CouponRequest> GetDiscount(string productId)
        {
            try
            {
                var request = new GetDiscountRequest { ProductId = productId };
                var discount = await _grpcDiscountServiceClient.GetDiscountAsync(request);
                return discount;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
//Reference : https://www.youtube.com/watch?v=ie20O6tFK94&list=PLqCbg_KAOnCfmkrOFLBvCqR6jipDhNs0w&index=62
