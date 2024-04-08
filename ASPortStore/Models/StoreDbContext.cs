using Microsoft.EntityFrameworkCore;

namespace ASPortStore.Models;

public class StoreDBContext(DbContextOptions<StoreDBContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
}
