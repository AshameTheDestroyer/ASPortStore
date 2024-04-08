using Moq;
using ASPortStore.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using ASPortStore.Views.Shared.Components.NavigationMenu;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPortStore.Tests;

public class NavigationMenuViewComponentTests
{
    [Fact]
    public void CanSelectCategories()
    {
        // Arrange
        Mock<IStoreRepository> mock = new();
        mock.Setup(mock => mock.Products).Returns((new Product[] {
            new() { ProductID = 1, Name = "P1", Category = "Apples" },
            new() { ProductID = 2, Name = "P2", Category = "Apples" },
            new() { ProductID = 3, Name = "P3", Category = "Plums" },
            new() { ProductID = 4, Name = "P4", Category = "Oranges" },
        }).AsQueryable());

        NavigationMenuViewComponent target = new(mock.Object);

        // Act
        string[] results = ((IEnumerable<string>?)(target.Invoke() as ViewViewComponentResult) ?.ViewData?.Model ?? []).ToArray();

        // Assert
        Assert.True(Enumerable.SequenceEqual(["Apples", "Oranges", "Plums"], results));
    }

    [Fact]
    public void IndicatesSelectedCategory()
    {
        // Arrange
        string categoryToSelect = "Apples";
        Mock<IStoreRepository> mock = new();
        mock.Setup(mock => mock.Products).Returns((new Product[] {
            new() { ProductID = 1, Name = "P1", Category = "Apples" },
            new() { ProductID = 4, Name = "P2", Category = "Oranges" },
        }).AsQueryable());

        NavigationMenuViewComponent target = new(mock.Object)
        {
            ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
                },
            },
        };
        target.RouteData.Values["category"] = categoryToSelect;

        // Action
        string? result = (string?)(target.Invoke() as ViewViewComponentResult)?.ViewData?["SelectedCategory"];
        
        // Assert
        Assert.Equal(categoryToSelect, result);
    }
}