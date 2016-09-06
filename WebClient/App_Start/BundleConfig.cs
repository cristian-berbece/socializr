using System.Web;
using System.Web.Optimization;

namespace Socializr
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/lib/jquery-{version}.js",
                "~/Scripts/lib/jquery.validate*",
                "~/Scripts/lib/modernizr-*",
                "~/Scripts/lib/bootstrap.js",
                "~/Scripts/lib/bootstrap-datepicker.js",
                "~/Scripts/lib/respond.js",
                "~/Scripts/lib/select2.min.js",
                "~/Scripts/lib/handlebars-v4.0.5.js",
                "~/Scripts/app/Global/literals.js",
                "~/Scripts/app/Global/friendRequestNotification.js",
                "~/Scripts/app/Global/searchBar.js"));

            bundles.Add(new StyleBundle("~/Content/css/css").Include(
                "~/Content/css/bootstrap-yeti.css",
                "~/Content/css/bootstrap-datepicker.css",
                "~/Content/css/main.css",
                "~/Content/css/like-button.css",
                "~/Content/css/select2.min.css"));
        }
    }
}
