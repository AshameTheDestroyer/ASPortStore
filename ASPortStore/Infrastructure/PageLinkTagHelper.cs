using Microsoft.AspNetCore.Mvc;
using ASPortStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ASPortStore.Infrastructure;

[HtmlTargetElement("div", Attributes = "page-model")]
public class PageLinkTagHelper(IUrlHelperFactory urlHelperFactory) : TagHelper
{
    private readonly IUrlHelperFactory urlHelperFactory = urlHelperFactory;

    public string? ClassName { get; set; } = string.Empty;
    public string? Id { get; set; } = string.Empty;

    [ViewContext, HtmlAttributeNotBound]
    public ViewContext? ViewContext { get; set; }
    public PagingInfo? PageModel { get; set; }
    public string? PageAction { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext is null || PageModel is null) { return; }
        
        IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
        TagBuilder result = new("div");
        result.AddCssClass("paginator");
        
        if (ClassName != null) { result.AddCssClass(ClassName); }
        if (Id != null) { result.GenerateId(Id, "_"); }

        for (int i = 1; i <= PageModel.TotalPages; i++)
        {
            TagBuilder tag = new("a");
            tag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
            tag.InnerHtml.Append(i.ToString());
            result.InnerHtml.AppendHtml(tag);
        }

        output.Content.AppendHtml(result.InnerHtml);
    }
}