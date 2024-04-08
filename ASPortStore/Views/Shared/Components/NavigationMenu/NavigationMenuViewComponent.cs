using ASPortStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPortStore.Views.Shared.Components.NavigationMenu;

public class NavigationMenuViewComponent(IStoreRepository storeRepository_) : ViewComponent
{
    private readonly IStoreRepository storeRepository = storeRepository_;

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
