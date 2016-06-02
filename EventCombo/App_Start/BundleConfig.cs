using System.Web;
using System.Web.Optimization;

namespace EventCombo
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery11.2").Include(
                       "~/Scripts/jquery-1.11.2.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.css"));
            #region ThirdPartycss
            bundles.Add(new StyleBundle("~/Content/timepicker").Include(
                   
                    "~/Content/jquery.timepicker.css",
                   "~/Content/ec-select.css",
                   "~/Content/eventcombo.css"));
            bundles.Add(new StyleBundle("~/Content/fontawsome").Include(
                    "~/Content/font-awesome.min.css"));
            
            bundles.Add(new StyleBundle("~/Content/summernote").Include(

                  "~/Content/summernote.css"));
            bundles.Add(new StyleBundle("~/Content/multiselect").Include(

                 "~/Content/bootstrap-multiselect.css"));
            bundles.Add(new StyleBundle("~/Content/eventcombonew").Include(

               "~/Content/eventcombo-new.css"));
            bundles.Add(new StyleBundle("~/Content/mapplaces").Include(

              "~/Content/MapPlaces.css"));
            bundles.Add(new StyleBundle("~/Content/jquery_ui").Include(

            "~/Content/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/Content/jquery_ui").Include(

         "~/Content/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/Content/jqueryfiller").Include(
                 "~/Content/Filer/jquery.filer.css",
                 "~/Content/Filer/jquery.filer-dragdropbox-theme.css",
                 "~/Content/Filer/assets/fonts/jquery.filer-icons/jquery-filer.css"));
            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include(

            "~/Content/datepicker.css"));
            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                  "~/Scripts/dropzone/css/basic.css",
                  "~/Scripts/dropzone/css/dropzone.css"));
            #endregion
            #region ThirdPartyScripts
            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/dropzone.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryfiller").Include(
                   "~/Scripts/filer/jquery.filer.js"));
            bundles.Add(new ScriptBundle("~/bundles/masked").Include(
             "~/Scripts/jquery.masked.js"));
            bundles.Add(new ScriptBundle("~/bundles/masked_input").Include(
           "~/Scripts/jquery.maskedinput.min.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/timezonescripts").Include(
                    "~/Scripts/jquery.timepicker.js",
                    "~/Scripts/jquery.cookie.js",
                    "~/Scripts/ec-select.js"));
            bundles.Add(new ScriptBundle("~/bundles/googleapis").Include(
                   "~/Scripts/jquery.googleapis-places.js"));
            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                    "~/Scripts/Validation.js",
                    "~/Scripts/jquery-ui.js"));
            bundles.Add(new ScriptBundle("~/bundles/summernote").Include(
                   "~/Scripts/summernote.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/boostrapmulti").Include(
              "~/Scripts/bootstrap-multiselect.js",
              "~/Scripts/DateTimePicker.js"
              
              ));
           // bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
           // "~/Scripts/DateTimePicker.js"
         
           //));
            #endregion




        }
    }
}
