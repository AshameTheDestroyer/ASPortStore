﻿using System.Text.Encodings.Web;
using ASPortStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ASPortStore.Infrastructure;

[HtmlTargetElement("section", Attributes = "page-model")]
public class PaginatorTagHelper(IUrlHelperFactory urlHelperFactory) : TagHelper
{
    private readonly IUrlHelperFactory urlHelperFactory = urlHelperFactory;

    [ViewContext, HtmlAttributeNotBound]
    public ViewContext? ViewContext { get; set; }
    public PageInfo? PageModel { get; set; }
    public string? PageAction { get; set; }

    [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
    public Dictionary<string, object> PageUrlValues { get; set; } = [];

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext is null || PageModel is null)
        {
            return;
        }

        IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
        TagBuilder result = new("div");

        for (int i = 0; i < PageModel.TotalPages; i++)
        {
            TagBuilder tag = new("a");

            PageUrlValues["page"] = i + 1;
            tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

            tag.InnerHtml.Append((i + 1).ToString());

            if (i + 1 == PageModel.CurrentPage)
            {
                tag.Attributes["data-is-selected-page"] = "true";
            }

            result.InnerHtml.AppendHtml(tag);
        }

        output.AddClass("paginator", HtmlEncoder.Default);
        output.Content.AppendHtml(result.InnerHtml);
    }
}
