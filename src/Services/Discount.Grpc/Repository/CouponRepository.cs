using Dapper;
using Discount.Grpc.Models;
using Npgsql;

namespace Discount.Grpc.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IConfiguration _configuration;
        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
                connection.Open();

                var sql = @"INSERT INTO Coupon(ProductId, ProductName, Description,Amount) VALUES(@ProductId,@ProductName,@Description,@Amount);";
                var affectedRow =await connection.ExecuteAsync(sql, new 
                { 
                    ProductId = coupon.ProductId, 
                    ProductName = coupon.ProductName, 
                    Description = coupon.Description, 
                    Amount = coupon.Amount 
                });

                return affectedRow > 0 ? true: false;   
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDiscount(string productId)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
                connection.Open();

                var sql = @"Delete FROM Coupon Where ProductId=@ProductId";
                var affectedRow = await connection.ExecuteAsync(sql, new { ProductId = productId });

                return affectedRow > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
                connection.Open();

                var sql = @"SELECT * FROM Coupon WHERE ProductId = @ProductId";
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { ProductId= productId });
                if(coupon == null)
                {
                    return new Coupon() { Amount = 0, ProductName = "No Discount", ProductId = "No Discount", Description = "No Discount" };
                }
                return coupon;  
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
                connection.Open();

                var sql = @"UPDATE Coupon SET ProductId=@ProductId, ProductName= @ProductName, Description=@Description,Amount=@Amount";
                var affectedRow = await connection.ExecuteAsync(sql, new
                {
                    ProductId = coupon.ProductId,
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                });

                return affectedRow > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
