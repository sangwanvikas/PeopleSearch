﻿using System.Web;
using System.Web.Optimization;

namespace PeopleSearch
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Common/css").Include(
                      "~/Common/css/register.css",
                       "~/Common/css/search.css",
                        "~/Common/css/result.css"));



            bundles.Add(new ScriptBundle("~/bundles/common/registerJs").Include(
                                  "~/Common/js/register.js"));

            bundles.Add(new ScriptBundle("~/bundles/common/searchJs").Include(
                                 "~/Common/js/search.js"));
        }
    }
}
