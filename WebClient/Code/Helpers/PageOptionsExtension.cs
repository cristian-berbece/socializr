using Socializr.Code.Pagination;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Socializr.Code.Helpers
{
    public static class PageOptionsExtension
    {
        public static HtmlString PageOptions<T>(this HtmlHelper helper, Pager<T> pager, string actionName)
        {
            var pageNumber = pager.CurrentPageNumber;
            var finalResult = string.Empty;
            MvcHtmlString link;

            if (pager.CurrentPageNumber != 1)
            {
                link = helper.ActionLink("First Page", actionName, new { pageNumber = 1 });
                finalResult += link.ToHtmlString();

                link = helper.ActionLink("Previous Page", actionName, new { pageNumber = pageNumber - 1 });
                finalResult += link.ToHtmlString();
            }

            if (pager.CurrentPageNumber != pager.LastPageNumber)
            {

                link = helper.ActionLink("Next Page", actionName, new { pageNumber = pageNumber + 1 });
                finalResult += link.ToHtmlString();

                link = helper.ActionLink("Last Page", actionName, new { pageNumber = pager.LastPageNumber });
                finalResult += link.ToHtmlString();

            }

            else
            {

                finalResult += "This is the last page";
            }
            return new HtmlString(finalResult);
        }
    }
}