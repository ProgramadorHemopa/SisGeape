using System.Web.Optimization;

namespace CMM.Projects.Apresentation
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862

        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));
            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"
                      , "~/Scripts/toastr.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/Grid").Include(
                    "~/Scripts/Grid/DataTables-1.10.16/js/jquery.dataTables.min.js"
                    , "~/Scripts/Grid/DataTables-1.10.16/js/dataTables.bootstrap4.min.js"
                    , "~/Scripts/Grid/Responsive-2.2.1/js/dataTables.responsive.min.js",
                    "~/Scripts/Grid/Responsive-2.2.1/js/responsive.bootstrap4.min.js"
              ));



            bundles.Add(new StyleBundle("~/BundledStyles/css").Include(
                     "~/assets/plugins/simplebar/css/simplebar.css",
                    "~/assets/css/bootstrap.min.css",
                    "~/assets/css/animate.css",
                             "~/assets/css/sidebar-menu.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
               "~/assets/js/popper.min.js",
             "~/assets/js/bootstrap.min.js",
               "~/assets/plugins/simplebar/js/simplebar.js",
               "~/assets/js/waves.js",
               "~/assets/js/sidebar-menu.js",
               "~/assets/js/app-script.js"
         ));

            BundleTable.EnableOptimizations = true;//compila os css/js

        }
    }
}