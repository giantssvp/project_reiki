using System.Web;
using System.Web.Optimization;

namespace project_reiki
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.1.3.1.pack.js",
                        "~/Scripts/jquery.tabs.pack.js",
                        "~/Scripts/jquery.history_remote.pack.js"
                        ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/jquery.tabs.css",
                      "~/Content/social_media.css",                      
                      "~/Content/templatemo_style.css"));
        }
    }
}
