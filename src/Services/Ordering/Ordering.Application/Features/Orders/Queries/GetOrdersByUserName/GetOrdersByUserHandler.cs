using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByUserName
{
    public class GetOrdersByUserHandler : IRequestHandler<GetOrdersByUserQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrdersByUserHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        } 
        public async Task<List<OrderDto>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrderDto>>(order);
        }
    }
}

/*
 * Cancellation token:
 A cancellation token is used for cancelling asynchronous database operations when necessary. 
It ensures a better user experience in application by allowing users to cancel long running database operations
 */