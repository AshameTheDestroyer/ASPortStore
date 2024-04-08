using ASPortStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPortStore.Controllers;

public class OrderController(IOrderRepository repository_, Cart cart_) : Controller
{
    private IOrderRepository repository = repository_;
    private Cart cart = cart_;

    public ViewResult Checkout() => View(new Order());

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        if (cart.Lines.Count == 0)
        {
            ModelState.AddModelError("", "Sorry, your cart is empty.");
        }

        if (ModelState.IsValid)
        {
            order.Lines = [.. cart.Lines];
            repository.SaveOrder(order);
            cart.Clear();
            return RedirectToPage("/CompletedMessage", new { orderID = order.ID });
        }

        return View();
    }
}
