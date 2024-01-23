namespace ASPortStore.Models.ViewModels;

public class ProductsListViewModel
{
    public IEnumerable<Product> Products { get; set; } = [];
    public PagingInfo PagingInfo { get; set; } = new();
    public string? CurrentCategory { get; set; }
}