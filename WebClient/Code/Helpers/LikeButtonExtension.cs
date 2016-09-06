using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Socializr.Code.Helpers
{
    public static class LikeButtonExtension
    {
        public static HtmlString LikeButton(this HtmlHelper helper, long idPost, bool isDislikeButton = false)
        {
            var htmlResult = "<button type='button' id = 'btn-set-like" + idPost.ToString() + "'";
            if (isDislikeButton)
            {
                htmlResult += "class='btn btn-sm btn-dislike btn-set-like'>";
                htmlResult += " <span class='glyphicon glyphicon-thumbs-down'></span> Dislike";
            }

            else
            {
                htmlResult += "class='btn btn-sm btn-like btn-set-like'>";
                htmlResult += " <span class='glyphicon glyphicon-thumbs-up'></span> Like";
            }
            htmlResult += "</button>";
            return new HtmlString(htmlResult);
        }
    }
}