using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Chappyware.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/third-party/jQuery/jquery-3.1.1.js"
                        , "~/Scripts/third-party/underscore/underscore.js"
                        , "~/Scripts/third-party/dataTables/jquery.dataTables.js"
                        , "~/Scripts/third-party/material/material.js"
                        , "~/Scripts/third-party/canvasjs/canvasjs.min.js"
                        , "~/Scripts/third-party/canvasjs/jquery.canvasjs.min.js"
                        , "~/Scripts/typescript/*.js"));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                        "~/Styles/third-party/dataTables/jquery.datatables.css"
                        , "~/Styles/third-party/material/material.css"
                        , "~/Styles/third-party/dataTables/dataTables.material.css"
                        , "~/Styles/*.css"
                ));
        }

    }
}