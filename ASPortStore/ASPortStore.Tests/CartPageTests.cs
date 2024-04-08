using System.Linq;
using System.Text;
using System.Text.Json;
using ASPortStore.Models;
using ASPortStore.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace ASPortStore.Tests;

public class CartPageTests
{
    [Fact]
    public void CanLoadCart()
    {
        // Arrange
        Product product1 = new() { ProductID = 1, Name = "P1" },
            product2 = new() { ProductID = 2, Name = "P2" };

        Mock<IStoreRepository> mockRepository = new();
        mockRepository
            .Setup(mock => mock.Products)
            .Returns((new Product[] { product1, product2 }).AsQueryable());

        // - create a cart
        Cart cart = new();
        cart.AddItem(product1, 2);
        cart.AddItem(product2, 1);

        // Action
        CartModel cartModel = new(mockRepository.Object, cart);
        cartModel.OnGet("myUrl");

        //Assert
        Assert.Equal(2, cartModel.Cart?.Lines.Count);
        Assert.Equal("myUrl", cartModel.ReturnUrl);
    }

    [Fact]
    public void CanUpdateCart()
    {
        // Arrange
        Mock<IStoreRepository> mockRepository = new();
        mockRepository
            .Setup(mock => mock.Products)
            .Returns(
                (
                    new Product[]
                    {
                        new() { ProductID = 1, Name = "P1" }
                    }
                ).AsQueryable()
            );

        Cart cart = new();

        // Action
        CartModel cartModel = new(mockRepository.Object, cart);
        cartModel.OnPost(1, "myUrl");

        //Assert
        Assert.Single(cart.Lines);
        Assert.Equal("P1", cart.Lines.First().Product.Name);
        Assert.Equal(1, cart.Lines.First().Quantity);
    }
}
