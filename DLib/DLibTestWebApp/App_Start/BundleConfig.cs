﻿using System.Web;
using System.Web.Optimization;

namespace DLibTestWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            #region CSS

            bundles.Add(new StyleBundle("~/Content/bootstrap_css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/font-awesome.css",
                "~/Content/Site.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css/select2_css").Include(
                "~/Content/css/select2.css",
                "~/Content/css/select2-bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/datatables_css").Include(
                //"~/Content/jquery.dataTables.css",
                "~/Content/DataTables/dataTables.bootstrap.css"
                ));

            #endregion

            #region Javascript

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                      "~/Scripts/select2.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/Scripts/Site.js"));

            #endregion


#if (!DEBUG)
                        {
                            BundleTable.EnableOptimizations = true;
                        }
#endif
        }
    }
}
