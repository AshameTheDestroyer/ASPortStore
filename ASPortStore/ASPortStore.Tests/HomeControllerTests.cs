using ASPortStore.Controllers;
using ASPortStore.Models;
using ASPortStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ASPortStore.Tests;

public class HomeControllerTests
{
    [Fact]
    public void CanUseRepository()
    {
        // Arrange
        Mock<IStoreRepository> mock = new();
        mock.Setup(mock => mock.Products)
            .Returns(
                (
                    new Product[]
                    {
                        new() { ProductID = 1, Name = "P1" },
                        new() { ProductID = 2, Name = "P2" },
                    }
                ).AsQueryable()
            );

        HomeController controller = new(mock.Object);

        // Act
        ProductsListViewModel result =
            controller.Index(category: null)?.ViewData.Model as ProductsListViewModel ?? new();

        // Assert
        Product[] products = result.Products.ToArray();
        Assert.Equal(2, products.Length);
        Assert.Equal("P1", products[0].Name);
        Assert.Equal("P2", products[1].Name);
    }

    [Fact]
    public void CanPaginate()
    {
        // Arrange
        Mock<IStoreRepository> mock = new();
        mock.Setup(mock => mock.Products)
            .Returns(
                (
                    new Product[]
                    {
                        new() { ProductID = 1, Name = "P1" },
                        new() { ProductID = 2, Name = "P2" },
                        new() { ProductID = 3, Name = "P3" },
                        new() { ProductID = 4, Name = "P4" },
                        new() { ProductID = 5, Name = "P5" },
                    }
                ).AsQueryable()
            );

        HomeController controller = new(mock.Object) { PageSize = 3 };

        // Act
        ProductsListViewModel result =
            controller.Index(category: null, page: 2)?.ViewData.Model as ProductsListViewModel
            ?? new();

        // Assert
        Product[] products = result.Products.ToArray();
        Assert.Equal(2, products.Length);
        Assert.Equal("P4", products[0].Name);
        Assert.Equal("P5", products[1].Name);
    }

    [Fact]
    public void CanSendPaginationViewModel()
    {
        // Arrange
        Mock<IStoreRepository> mock = new();
        mock.Setup(mock => mock.Products)
            .Returns(
                (
                    new Product[]
                    {
                        new() { ProductID = 1, Name = "P1" },
                        new() { ProductID = 2, Name = "P2" },
                        new() { ProductID = 3, Name = "P3" },
                        new() { ProductID = 4, Name = "P4" },
                        new() { ProductID = 5, Name = "P5" },
                    }
                ).AsQueryable()
            );

        // Arrange
        HomeController controller = new(mock.Object) { PageSize = 3 };

        // Act
        ProductsListViewModel result =
            controller.Index(category: null, page: 2)?.ViewData.Model as ProductsListViewModel
            ?? new();

        // Assert
        PageInfo pageInfo = result.PageInfo;
        Assert.Equal(2, pageInfo.CurrentPage);
        Assert.Equal(3, pageInfo.ItemsPerPage);
        Assert.Equal(5, pageInfo.TotalItems);
        Assert.Equal(2, pageInfo.TotalPages);
    }

    [Fact]
    public void GenerateCategorySpecificProductCount()
    {
        // Arrange
        Mock<IStoreRepository> mock = new();
        mock.Setup(mock => mock.Products)
            .Returns(
                (
                    new Product[]
                    {
                        new()
                        {
                            ProductID = 1,
                            Name = "P1",
                            Category = "Cat1"
                        },
                        new()
                        {
                            ProductID = 2,
                            Name = "P2",
                            Category = "Cat2"
                        },
                        new()
                        {
                            ProductID = 3,
                            Name = "P3",
                            Category = "Cat1"
                        },
                        new()
                        {
                            ProductID = 4,
                            Name = "P4",
                            Category = "Cat2"
                        },
                        new()
                        {
                            ProductID = 5,
                            Name = "P5",
                            Category = "Cat3"
                        },
                    }
                ).AsQueryable()
            );

        HomeController target = new(mock.Object) { PageSize = 3 };

        static ProductsListViewModel? GetModel(ViewResult result) =>
            result?.ViewData?.Model as ProductsListViewModel;

        // Action
        int? result1 = GetModel(target.Index("Cat1"))?.PageInfo.TotalItems,
            result2 = GetModel(target.Index("Cat2"))?.PageInfo.TotalItems,
            result3 = GetModel(target.Index("Cat3"))?.PageInfo.TotalItems,
            resultAll = GetModel(target.Index(null))?.PageInfo.TotalItems;

        // Assert
        Assert.Equal(2, result1);
        Assert.Equal(2, result2);
        Assert.Equal(1, result3);
        Assert.Equal(5, resultAll);
    }
}
