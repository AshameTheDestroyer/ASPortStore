using Microsoft.EntityFrameworkCore;

namespace ASPortStore.Models;

public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}