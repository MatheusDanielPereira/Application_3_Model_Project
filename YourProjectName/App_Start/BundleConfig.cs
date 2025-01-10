using System.Web;
using System.Web.Optimization;

namespace YourProjectName
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Js").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/modernizr-*",
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/footable_customized.js"));

            bundles.Add(new ScriptBundle("~/bundles/MetsoJs").Include(
                "~/Scripts/metso.js"));

            bundles.Add(new ScriptBundle("~/bundles/MetsoChart").Include(
                "~/Scripts/Chart.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/Css").Include(
                "~/Content/bootstrap.css",
                "~/Content/animate.css",
                "~/Content/fontawesome-all.min.css",
                "~/Content/font-awesome-4.min.css",//Needed for footable icons
                "~/Content/footable.standalone.min.css"));

            bundles.Add(new StyleBundle("~/Content/MetsoCss").Include(
                "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Login").Include(
               "~/Content/fontawesome-all.min.css",
               "~/Content/metso-login.css"));

            bundles.Add(new StyleBundle("~/Content/Home").Include(
                "~/Content/metso-home.css"));
        }
    }
}
