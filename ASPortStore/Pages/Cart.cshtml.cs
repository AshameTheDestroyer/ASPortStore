using ASPortStore.Infrastructure;
using ASPortStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPortStore.Pages;

public class CartModel(IStoreRepository repository_, Cart cart) : PageModel
{
    private readonly IStoreRepository repository = repository_;

    public Cart Cart { get; set; } = cart;
    public string ReturnUrl { get; set; } = "/";

    public void OnGet(string returnUrl)
    {
        ReturnUrl = returnUrl ?? "/";
    }

    public IActionResult OnPost(long productId, string returnUrl)
    {
        Product? product = repository.Products.FirstOrDefault(product_ =>
            product_.ProductID == productId
        );

        if (product != null)
        {
            Cart.AddItem(product, 1);
        }

        return RedirectToPage(new { returnUrl });
    }

    public IActionResult OnPostRemove(long productId, string returnUrl)
    {
        Cart.RemoveLine(Cart.Lines.First(line => line.Product.ProductID == productId).Product);
        return RedirectToPage(new { returnUrl });
    }
}
