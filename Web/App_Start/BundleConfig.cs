using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.12.4.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/fonts.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/style.css",
                      "~/Content/simple-line-icons.css",
                      "~/Content/custom.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/fontawesone").Include(
                      "~/plugins/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/bundles/progresscss").Include(
                      "~/plugins/pace/pace.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/progressjs").Include(
                      "~/plugins/pace/pace.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ie8").Include(
                      "~/Scripts/html5shiv.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/scripts.js",
                      "~/plugins/nanoscrollerjs/jquery.nanoscroller.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ie9").Include(
                      "~/plugins/fast-click/fastclick.min.js",
                      "~/plugins/metismenu/metismenu.min.js",
                      "~/plugins/screenfull/screenfull.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/promise").Include(
                      "~/Scripts/es6-promise.min.js",
                      "~/Scripts/es6-promise.auto.min.js"));
        }
    }
}
