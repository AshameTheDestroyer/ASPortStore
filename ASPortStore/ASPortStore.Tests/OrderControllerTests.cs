using ASPortStore.Controllers;
using ASPortStore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ASPortStore.Tests;

public class OrderControllerTests
{
    [Fact]
    public void CannotCheckoutEmptyCart()
    {
        // Arrange
        Mock<IOrderRepository> mock = new();
        Cart cart = new();
        Order order = new();
        OrderController target = new(mock.Object, cart);

        // Act
        ViewResult? result = target.Checkout(order) as ViewResult;

        // Assert
        mock.Verify(mock_ => mock_.SaveOrder(It.IsAny<Order>()), Times.Never);
        Assert.True(string.IsNullOrEmpty(result?.ViewName));
        Assert.False(result?.ViewData.ModelState.IsValid);
    }

    [Fact]
    public void CannotCheckoutInvalidShippingDetails()
    {
        // Arrange
        Mock<IOrderRepository> mock = new();
        Cart cart = new();
        cart.AddItem(new Product(), 1);
        OrderController target = new(mock.Object, cart);
        target.ModelState.AddModelError("error", "error");

        // Act
        ViewResult? result = target.Checkout(new Order()) as ViewResult;

        // Assert
        mock.Verify(mock_ => mock_.SaveOrder(It.IsAny<Order>()), Times.Never);
        Assert.True(string.IsNullOrEmpty(result?.ViewName));
        Assert.False(result?.ViewData.ModelState.IsValid);
    }

    [Fact]
    public void CanCheckoutAndSubmitOrder()
    {
        // Arrange
        Mock<IOrderRepository> mock = new();
        Cart cart = new();
        cart.AddItem(new Product(), 1);
        OrderController target = new(mock.Object, cart);

        // Act
        RedirectToPageResult? result = target.Checkout(new Order()) as RedirectToPageResult;

        // Assert
        mock.Verify(mock_ => mock_.SaveOrder(It.IsAny<Order>()), Times.Once);
        Assert.Equal("/Completed", result?.PageName);
    }
}
