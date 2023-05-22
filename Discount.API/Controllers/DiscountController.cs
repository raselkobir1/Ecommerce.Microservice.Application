using CoreApiResponse;
using Discount.API.Models;
using Discount.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private readonly ICouponRepository _couponRepository;
        public DiscountController( ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDiscount(string productId)
        {
            try
            {
                var coupon = await _couponRepository.GetDiscount(productId);
                return CustomResult(coupon);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var isSaved = await _couponRepository.CreateDiscount(coupon);
                if (isSaved)
                {
                    return CustomResult("Coupon has been created.",coupon);
                }
                return CustomResult("Coupon Saved faild", coupon, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var isSaved = await _couponRepository.UpdateDiscount(coupon);
                if (isSaved)
                {
                    return CustomResult("Coupon has been modified.", coupon);
                }
                return CustomResult("Coupon modifued faild", coupon, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDiscount(string productId)
        {
            try
            {
                var isDeleted = await _couponRepository.DeleteDiscount(productId);
                if (isDeleted)
                {
                    return CustomResult("Coupon has been deleted.");
                }
                return CustomResult("Coupon deleted faild", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
