using Microsoft.EntityFrameworkCore;

namespace ASPortStore.Models;

public class EFOrderRepository(StoreDBContext context_) : IOrderRepository
{
    private readonly StoreDBContext context = context_;

    public IQueryable<Order> Orders =>
        context.Orders.Include(order => order.Lines).ThenInclude(line => line.Product);

    public void SaveOrder(Order order)
    {
        context.AttachRange(order.Lines.Select(line => line.Product));
        if (order.ID == 0)
        {
            context.Orders.Add(order);
        }
        context.SaveChanges();
    }
}
