namespace ASPortStore.Models;

public class EFStoreRepository(StoreDBContext context) : IStoreRepository
{
    private readonly StoreDBContext context = context;

    public IQueryable<Product> Products => context.Products;
}
