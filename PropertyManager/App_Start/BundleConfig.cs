using System.Web;
using System.Web.Optimization;

namespace PropertyManager
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new Bundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/boostrap/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap.css",
                     "~/Content/ui-bootstrap-csp.css",
                     "~/Content/font-awesome.css",
                     "~/Content/fullcalendar.css",
                     "~/app/Vendor/metisMenu.css",
                     "~/app/stylish-portfolio.css",
                     "~/app/main.css"));

            bundles.Add(new Bundle("~/bundles/propertymanagerapp/script").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/ng-file-upload.js",
                      "~/Scripts/angular-route.js",
                      "~/Scripts/angular-resource.js",
                      "~/Scripts/jquery-1.10.2.js",                     
                      "~/Scripts/bootstrap.js",                 
                      "~/Scripts/angular-ui/ui-bootstrap.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                      "~/Scripts/angular-animate.js",
                      "~/app/Vendor/metisMenu.js",
                      "~/app/Vendor/moment.js",
                      "~/app/Vendor/calendar.js",
                      "~/app/Vendor/fullcalendar.js",
                      "~/app/Vendor/gcal.js",                      
                      "~/app/Vendor/angular-modal-service.min.js",
                      "~/app/Vendor/Chart.min.js",
                      "~/app/Vendor/angular-chart.min.js",
                      "~/app/app.js",
                      //SERVICES
                      "~/app/Services/common.services.js",
                      "~/app/Services/announcementService.js",
                      "~/app/Services/apartmentService.js",
                      "~/app/Services/Directives.js",
                      "~/app/Services/employeeService.js",
                      "~/app/Services/facilityBookingService.js",
                      "~/app/Services/facilityService.js",
                      "~/app/Services/inventoryService.js",
                      "~/app/Services/leaseService.js",
                      "~/app/Services/loginService.js",
                      "~/app/Services/occupantService.js",
                      "~/app/Services/serviceService.js",
                      "~/app/Services/serviceRequestService.js",
                      "~/app/Services/tenantService.js",
                      "~/app/Services/unitService.js",
                      "~/app/Services/unitphotoService.js",
                      "~/app/Services/userProfile.js",
                      "~/app/Services/userService.js",
                      "~/app/Services/workOrderService.js",
                      //CONTROLLERS
                      "~/app/Controllers/announcementController.js",
                      "~/app/Controllers/apartmentController.js",
                      "~/app/Controllers/employeeController.js",
                      "~/app/Controllers/facilityBookingController.js",
                      "~/app/Controllers/facilityController.js",
                      "~/app/Controllers/headerController.js",
                      "~/app/Controllers/homeController.js",
                      "~/app/Controllers/inventoryController.js",
                      "~/app/Controllers/leaseController.js",
                      "~/app/Controllers/loginController.js",
                      "~/app/Controllers/occupantController.js",
                      "~/app/Controllers/registerController.js",
                      "~/app/Controllers/serviceController.js",
                      "~/app/Controllers/serviceRequestController.js",
                      "~/app/Controllers/tenantController.js",
                      "~/app/Controllers/unitController.js",
                      "~/app/Controllers/unitphotoController.js",
                      "~/app/Controllers/userController.js",
                      "~/app/Controllers/workOrderController.js",
                      "~/app/Controllers/workOrderManagerController.js"
            ));
        }
    }
}
