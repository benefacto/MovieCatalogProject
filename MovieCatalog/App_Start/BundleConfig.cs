using System.Web.Optimization;

namespace MovieCatalog
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/ui-grid.css",
                 "~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/bundles/Angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/ui-grid.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
