using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Helpers
{
    public static class Extensions
    {
        public static MvcHtmlString Enumerate(this HtmlHelper htmlHelper,
            params string[] items)
        {
            var builder = new StringBuilder("<ol>");

            foreach (var item in items)
            {
                builder.Append($"<li>{item}</li>");
            }

            builder.Append("</ol>");

            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString Image(this HtmlHelper htmlHelper,
            string source, int height, int width)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", source);
            builder.MergeAttribute("height", height.ToString());
            builder.MergeAttribute("width", width.ToString());

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}