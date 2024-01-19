using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Models;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService) 
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
            _mapper = mapper;   
        }  
        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            order.CreatedBy = "1";
            order.CreatedDate = DateTime.Now;   
            var isOrderPlaced = await _orderRepository.AddAsync(order);
            if (isOrderPlaced) {
                var email = new Email();
                email.To.Add(request.UserName);
                email.Subject = "Your Order has been placed.";
                email.Body = $"Dear {request.FirstName} {request.LastName} <br/><br/> Thank you for placed an order.<br/> Your order number is #{request.Id}";
                //var isSend = await _emailService.SendEmailAsync(email);
            }
            return isOrderPlaced;    
        }
    }
}
