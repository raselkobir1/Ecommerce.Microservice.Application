using MediatR;
namespace Ordering.Application.Features.Orders.Queries.GetOrdersByUserName
{
    public class GetOrdersByUserQuery: IRequest<List<OrderDto>>
    {
        public GetOrdersByUserQuery(string userName)
        {
            UserName = userName;      
        }
        public string UserName { get; set; }
    }
}
