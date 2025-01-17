using System.Web.Optimization;

namespace Online_Retail_Store
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap22.css",
                      "~/Content/site.css",
                      "~/Content/css/style.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/icomoon.css",
                      "~/Content/css/ionicons.min.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/magnific-popup.css",
                      "~/Content/css/flexslider.css",
                      "~/Content/css/owl.carousel.min.css",
                      "~/Content/css/owl.theme.default.min.css",
                     "~/Content/css/bootstrap-datepicker.css",
                     "~/Content/fonts/flaticon/font/flaticon.css",
                     "~/Content/css/style.css"));

            bundles.Add(new Bundle("~/bundles/ok").Include(
                      "~/Scripts/jquery-3.7.1.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js",
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Scripts/DataTables/dataTables.bootstrap4.js",
                        "~/Scripts/DataTables/dataTables.responsive.js"));
        }
    }
}
