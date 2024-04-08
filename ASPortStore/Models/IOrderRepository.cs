using System.Linq;

namespace ASPortStore.Models;

public interface IOrderRepository
{
    IQueryable<Order> Orders { get; }
    void SaveOrder(Order order);
}
