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

            bundles.Add(new StyleBundle("~/Content/angularHeader")
              .Include( "~/Content/AMaterial/angular-material.css",
                        "~/Content/AMaterial/gridiculous.css",
                        "~/Content/AMaterial/docs.css",
                        "~/Content/AMaterial/header-footer.css"));

            bundles.Add(new StyleBundle("~/Content/angularMain-CE")
              .Include( "~/Content/AMaterial/angularjs-color-picker.min.css",
                        "~/Content/AMaterial/material-datetimepicker.css",
                        "~/Content/AMaterial/animations.css",
                        "~/Content/AMaterial/ngGallery.css",
                        "~/Content/AMaterial/main.css"));
            bundles.Add(new StyleBundle("~/Content/AngularViewEvent")
              .Include( "~/Content/font-awesome.min.css",
                        "~/Content/AMaterial/owl.carousel.css",
                        "~/Content/AMaterial/ViewEvent.css"));

            bundles.Add(new ScriptBundle("~/Scripts/AMaterial/angularHeader")
              .Include( "~/Scripts/AMaterial/angular.js",
                        "~/Scripts/AMaterial/angular-animate.js",
                        "~/Scripts/AMaterial/angular-route.js",
                        "~/Scripts/AMaterial/angular-aria.js",
                        "~/Scripts/AMaterial/angular-material.js",
                        "~/Scripts/AMaterial/angular-messages.js",
                        "~/Scripts/AMaterial/angular-cookies.js",
                        "~/Scripts/AMaterial/tinymce.min.js",
                        "~/Scripts/AMaterial/tinycolor-min.js",
                        "~/Scripts/AMaterial/angular-tinymce/tinymce.min.js",
                        "~/Scripts/AMaterial/themes/modern/theme.min.js",
                        "~/Scripts/AMaterial/angularjs-color-picker.min.js",
                        "~/Scripts/AMaterial/moment.js",
                        "~/Scripts/AMaterial/angular-material-datetimepicker.js",
                        "~/Scripts/AMaterial/ngGallery.js",
                        "~/Scripts/AMaterial/demo.js",
                        "~/Scripts/AMaterial/ngRepeatOwlCarousel.js",
                        "~/Scripts/AMaterial/classie.js",
                        "~/Scripts/AMaterial/EventComboApp.js",
                        "~/Scripts/AMaterial/header.js"));
            bundles.Add(new ScriptBundle("~/Scripts/AMaterial/viewEvent")
              .Include("~/Scripts/AMaterial/owl.carousel.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/AMaterial/viewEventFooter")
              .Include( "~/Scripts/AMaterial/infobox.js",
                        "~/Scripts/AMaterial/map.js",
                        "~/Scripts/AMaterial/ViewEvent.js",
                        "~/Scripts/socialshare.js"));
        }
    }
}
