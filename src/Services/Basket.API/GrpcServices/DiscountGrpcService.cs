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
            var request = new GetDiscountRequest { ProductId = productId };
            var discount = await _grpcDiscountServiceClient.GetDiscountAsync(request);
            return discount;
        }
    }
}
