using Basket.API.Models;
using Basket.API.Repository;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBusketRepository _busketRepository;
        public BasketController(IBusketRepository busketRepository)
        {
            _busketRepository = busketRepository;   
        }
        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<IActionResult>GetBusket(string userName)
        {
            try
            {
                var basket = await _busketRepository.GetBasket(userName);
                return CustomResult("Basket data load successfully",basket);  
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof (ShoppingCart),(int)HttpStatusCode.OK)] 
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            try
            {
                return CustomResult("Basket modied done.", await _busketRepository.UpdateBasket(shoppingCart));
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        public async Task<IActionResult>DeleteBasket(string userName)
        {
            try
            {
                await _busketRepository.DeleteBasket(userName);
                return CustomResult("Basket has been deleted.");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
