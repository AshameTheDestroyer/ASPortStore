namespace ASPortStore.Models.ViewModels;

public class ProductsListViewModel
{
    public IEnumerable<Product> Products { get; set; } = [];
    public PageInfo PageInfo { get; set; } = new();
    public string? CurrentCategory { get; set; }
}
