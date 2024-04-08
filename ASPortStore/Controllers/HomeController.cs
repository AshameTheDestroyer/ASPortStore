using ASPortStore.Models;
using ASPortStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPortStore.Controllers;

public class HomeController(IStoreRepository storeRepository) : Controller
{
    private readonly IStoreRepository storeRepository = storeRepository;

    public int PageSize { get; set; } = 6;

    [HttpGet("")]
    [HttpGet("{category:alpha}Category")]
    [HttpGet("{category:alpha}Category/Page{page:int}")]
    [HttpGet("Page{page:int}")]
    public ViewResult Index(string? category, int page = 1) =>
        View(
            new ProductsListViewModel
            {
                Products = storeRepository
                    .Products.OrderBy(product => product.ProductID)
                    .Where(product => category == null || product.Category == category)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = storeRepository
                        .Products.Where(product => category == null || product.Category == category)
                        .Count(),
                },
                CurrentCategory = category,
            }
        );
}
