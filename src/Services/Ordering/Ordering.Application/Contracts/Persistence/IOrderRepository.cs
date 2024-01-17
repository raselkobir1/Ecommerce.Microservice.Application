using EF.Core.Repository.Interface.Repository;
using Ordering.Domain.Models;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository: ICommonRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
