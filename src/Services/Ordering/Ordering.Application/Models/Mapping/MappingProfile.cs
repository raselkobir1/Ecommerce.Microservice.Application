using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersByUserName;
using Ordering.Domain.Models;

namespace Ordering.Application.Models.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
             CreateMap<Order, OrderDto>().ReverseMap();
             CreateMap<Order, CreateOrderCommand>().ReverseMap();
        }
    }
}
