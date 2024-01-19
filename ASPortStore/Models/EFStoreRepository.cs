namespace ASPortStore.Models;

public class EFStoreRepository(StoreDbContext context) : IStoreRepository
{
    private readonly StoreDbContext context = context;

    public IQueryable<Product> Products => context.Products;
}