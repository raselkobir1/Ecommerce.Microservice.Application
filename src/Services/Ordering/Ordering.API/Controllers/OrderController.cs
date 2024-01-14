using CoreApiResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Queries.GetOrdersByUserName;
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
    }
}
