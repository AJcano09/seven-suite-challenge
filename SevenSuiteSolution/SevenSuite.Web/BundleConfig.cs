
using System.Web.Optimization;

namespace SevenSuite.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
           .Include("~/scripts/vendor/jquery1.8.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                .Include("~/scripts/vendor/jquery-ui.js"));

            // core
            bundles.Add(new ScriptBundle("~/bundles/app-core")
                .Include(
                    "~/scripts/app/app.config.js",
                    "~/scripts/app/ApiClient.js",
                    "~/scripts/app/app.core.js",
                    "~/scripts/app/common.js",
                    "~/scripts/app/services/*.js"
                ));

            // login
            bundles.Add(new ScriptBundle("~/bundles/login")
                .Include("~/scripts/app/pages/login.page.js"));

            // client
            bundles.Add(new ScriptBundle("~/bundles/clientes")
                .Include("~/scripts/app/pages/clientes.page.js"));

            bundles.Add(new StyleBundle("~/content/css")
                .Include(
                    "~/content/site.css",
                    "~/scripts/vendor/jquery-ui.css"
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}