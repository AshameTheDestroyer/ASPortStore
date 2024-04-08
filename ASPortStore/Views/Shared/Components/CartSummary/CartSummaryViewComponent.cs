using ASPortStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPortStore.Views.Shared.Components.CartSummary;

public class CartSummaryViewComponent(Cart cart_) : ViewComponent
{
    private readonly Cart cart = cart_;

    public IViewComponentResult Invoke() =>
        View<string>(
            (cart.Lines.Count == 0)
                ? ""
                : $"{cart.TotalQuantity} item{(cart.Lines.Count > 1 ? "s" : "")}: {cart.TotalPrice:c}"
        );
}
