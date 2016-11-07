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
                        , "~/Scripts/FantasyLeague.js"));
        }

    }
}