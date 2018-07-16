using System.Web;
using System.Web.Optimization;

namespace OnlineShop2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/app-css").Include(
                      "~/Assets/client/css/bootstrap.min.css",
                      "~/Assets/client/css/bootstrap-social.css",
                      "~/Assets/client/css/jquery-ui.css",
                      "~/Assets/client/css/fontawesome-all.min.css",
                      "~/Assets/client/css/style.css",
                      "~/Assets/client/css/slider.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/app-js").Include(
                "~/Assets/client/js/jquery-3.3.1.min.js",
                "~/Assets/client/js/jquery-ui.js",
                "~/Assets/client/js/bootstrap.min.js",
                "~/Assets/client/js/move-top.js",
                "~/Assets/client/js/easing.js",
                "~/Assets/client/js/startstop-slider.js"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
