using Basket.API.Models;
using Basket.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApiResponse;
using System.Net;
using Basket.API.GrpcServices;
using MassTransit;
using AutoMapper;
using EventBus.Messages.Events;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBusketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        public BasketController(IBusketRepository basketRepository, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
            _publishEndpoint = publishEndpoint; 
            _mapper = mapper;   
        }
        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var basket = await _basketRepository.GetBasket(userName);
                return CustomResult("Basket data load successfully",basket);  
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            try
            {
                foreach (var item in basket.Items)
                {
                    var discount = await _discountGrpcService.GetDiscount(item.ProductId);
                    if (discount != null)
                        item.Price -= discount.Amount;
                }
                var basketResponse = await _basketRepository.UpdateBasket(basket);
                return CustomResult("Basket modified done.", basketResponse);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            try
            {
                await _basketRepository.DeleteBasket(userName);
                return CustomResult("Basket has been deleted.");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _basketRepository.GetBasket(basketCheckout.UserName);
        if (basket == null) 
            {
                return CustomResult("Basket is empty.",HttpStatusCode.BadRequest);
            }
            //send checkout event to RabbitMQ
            var eventMassage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMassage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMassage);

            //Remove checkout item from basket. 
            await _basketRepository.DeleteBasket(basket.UserName);  

            return CustomResult("Order has been placed.s");
        }
    }
}
