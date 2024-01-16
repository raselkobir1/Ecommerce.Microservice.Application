using CoreApiResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersByUserName;
using Ordering.Domain.Models;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
              _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderByUserName(string userName)
        {
            try
            {
                var orders = await _mediator.Send(new GetOrdersByUserQuery(userName));
                return CustomResult("Order load successfully.",orders); 
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand orderCreateCommand)
        {
            try
            {
                var isOrderPlaced = await _mediator.Send(orderCreateCommand); 
                if (isOrderPlaced)
                    return CustomResult("Order successfully placed");
                return CustomResult("Order not placed");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand orderUpdateCommand)
        {
            try
            {
                var isOrderModified = await _mediator.Send(orderUpdateCommand);
                if (isOrderModified)
                    return CustomResult("Order successfully modified");
                return CustomResult("Order modified failed");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteOrder(int orderId) 
        {
            try
            {
                var isOrderDelete = await _mediator.Send(new DeleteOrderCommand { Id = orderId });
                if (isOrderDelete)
                    return CustomResult("Order successfully deleted");
                return CustomResult("Order deleted failed");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }
    }
}
