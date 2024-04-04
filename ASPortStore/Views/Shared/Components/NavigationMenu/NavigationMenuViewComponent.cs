using ASPortStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPortStore.Views.Shared.Components.NavigationMenu;

public class NavigationMenuViewComponent(IStoreRepository repository) : ViewComponent
{
    private readonly IStoreRepository storeRepository = repository;

    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedCategory = RouteData?.Values["category"];
        return View(
            storeRepository
                .Products.Select(product => product.Category)
                .Distinct()
                .OrderBy(product => product)
        );
    }
}
