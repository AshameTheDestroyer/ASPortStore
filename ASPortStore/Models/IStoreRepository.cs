namespace ASPortStore.Models;

public interface IStoreRepository
{
    IQueryable<Product> Products { get; }
}