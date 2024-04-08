using ASPortStore.Infrastructure;
using ASPortStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;

namespace ASPortStore.Tests;

public class PaginatorTagHelperTests
{
    [Fact]
    public void CanGeneratePageLinks()
    {
        // Arrange
        Mock<IUrlHelper> urlHelper = new();
        urlHelper
            .SetupSequence(mock => mock.Action(It.IsAny<UrlActionContext>()))
            .Returns("Test/Page1")
            .Returns("Test/Page2")
            .Returns("Test/Page3");

        Mock<IUrlHelperFactory> urlHelperFactory = new();
        urlHelperFactory
            .Setup(mock => mock.GetUrlHelper(It.IsAny<ActionContext>()))
            .Returns(urlHelper.Object);

        Mock<ViewContext> viewContext = new();
        PaginatorTagHelper pageLinkTagHelper =
            new(urlHelperFactory.Object)
            {
                PageModel = new PageInfo()
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10,
                },
                ViewContext = viewContext.Object,
                PageAction = "Test",
            };

        TagHelperContext context = new([], new Dictionary<object, object>(), "");
        Mock<TagHelperContent> content = new();
        TagHelperOutput output =
            new("div", [], (cache, encoder) => Task.FromResult(content.Object));

        // Act
        pageLinkTagHelper.Process(context, output);

        // Assert
        Assert.Equal(
            @"<a href=""Test/Page1"">1</a>"
                + @"<a href=""Test/Page2"">2</a>"
                + @"<a href=""Test/Page3"">3</a>",
            output.Content.GetContent()
        );
    }
}
