//using Library.Models;
using LibraryDomain.Core.Links;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Library.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString LinksSection(this HtmlHelper helper, string Caption, string Action, string Controler, IEnumerable<ILink> Links)
        {
            StringBuilder sb = new StringBuilder();

            TagBuilder label = new TagBuilder("label");
            label.AddCssClass("control-label");
            label.InnerHtml = Caption;//"Автор по сериям";

            TagBuilder name = new TagBuilder("div");
            name.AddCssClass("col-md-3");
            name.InnerHtml = label.ToString();

            TagBuilder captionSection = new TagBuilder("div");
            captionSection.AddCssClass("row");
            captionSection.AddCssClass("form-group");
            captionSection.InnerHtml = name.ToString();

            sb.AppendLine(captionSection.ToString());

            if (Links.Count() == 0)
                return new MvcHtmlString(sb.ToString());

            var classes = new { @class = "btn btn-default col-md-3 col-sm-4 col-xs-12 truncate-text margin-all" };
            string linksSection = MakeButtonsSection(helper, Action, Controler, Links, classes);
            if (linksSection.Length > 0)
                sb.AppendLine(linksSection.ToString());

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString LinkButtons(this HtmlHelper helper, string Action, string Controler, IEnumerable<ILink> Links)
        {
            var classes = new { @class = "btn btn-default col-md-3 col-sm-4 col-xs-12 truncate-text margin-all" };
            string linksSection = MakeButtonsSection(helper, Action, Controler, Links, classes);
            return new MvcHtmlString(linksSection.ToString());
        }


        private static string MakeButtonsSection(HtmlHelper helper, string Action, string Controler, IEnumerable<ILink> Links, object Classes)
        {
            var ajaxOptions = new AjaxOptions() { HttpMethod = "Get", InsertionMode = InsertionMode.Replace, UpdateTargetId = "ContentBody" };

            AjaxHelper ajax = new AjaxHelper(helper.ViewContext, helper.ViewDataContainer, helper.RouteCollection);
            TagBuilder linksSection = new TagBuilder("div");

            linksSection.AddCssClass("row");
            linksSection.AddCssClass("form-group");

            foreach (ILink link in Links)
                linksSection.InnerHtml += ajax.ActionLink(link.Name.Trim(), Action, Controler, new { id = link.Id }, ajaxOptions, Classes).ToHtmlString();

            return linksSection.ToString();
        }
    }
}