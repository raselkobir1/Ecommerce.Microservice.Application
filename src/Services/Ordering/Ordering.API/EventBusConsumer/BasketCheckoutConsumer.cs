using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CreateOrder;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;
        private readonly IMapper _mapper;

        public BasketCheckoutConsumer(IMediator mediator, ILogger<BasketCheckoutConsumer> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;   
            _mapper = mapper;   
        }   
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var orderData = _mapper.Map<CreateOrderCommand>(context.Message);  
            bool isOrderConfirmed = await _mediator.Send(orderData);
            if (isOrderConfirmed)
                _logger.LogInformation($"Basket checkout event has been consumed. Order userName : {orderData.UserName}");
            else
                _logger.LogInformation($"Basket checkout event failed for {orderData.UserName}");
        }
    }
}
