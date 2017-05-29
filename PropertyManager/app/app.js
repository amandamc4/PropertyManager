angular.module("propertyManagerApp", ['ngRoute', 'ui.calendar', 'common.services', 'ui.bootstrap', 'ngAnimate', "chart.js"])
    .config(['$routeProvider', '$locationProvider', '$animateProvider',
        function ($routeProvider, $locationProvider, $animateProvider) {
            $locationProvider.hashPrefix('');
            $routeProvider
                .when('/index', {
                    controller: "",
                    templateUrl: 'app/Index.html'
                })
                .when('/home', {
                    controller: 'homeController',
                    templateUrl: 'app/Partials/home.html'
                })
                .when('/tenanthome', {
                    controller: 'homeController',
                    templateUrl: 'app/Partials/tenanthome.html'
                })
                .when('/login', {
                    controller: 'loginController',
                    templateUrl: 'app/Partials/Login/login.html'
                })
                .when('/register/', {
                    controller: 'registerController',
                    templateUrl: 'app/Partials/Login/register.html'
                })
                .when('/resetPassword/', {
                    controller: 'loginController',
                    templateUrl: 'app/Partials/Login/resetPassword.html'
                })
                .when('/registermanager/', {
                    controller: 'registerController',
                    templateUrl: 'app/Partials/Login/registerManager.html'
                })
                .when('/announcement/', {
                    controller: 'announcementController',
                    templateUrl: 'app/Partials/Announcement/announcement.html'
                })
                .when('/addannouncement/:announce_id?', {
                    controller: 'announcementController',
                    templateUrl: 'app/Partials/Announcement/announcementAdd.html'
                })
                .when('/facility/', {
                    controller: 'facilityController',
                    templateUrl: 'app/Partials/Facility/facility.html'
                })
                .when('/addfacility/:facility_id?', {
                    controller: 'facilityController',
                    templateUrl: 'app/Partials/Facility/facilityAdd.html'
                })
                 .when('/bookfacility/', {
                     controller: 'facilityBookingController',
                     templateUrl: 'app/Partials/Facility/facilityBookings.html'
                 })
                .when('/addbookfacility/:facilitybook_id?', {
                    controller: 'facilityBookingController',
                    templateUrl: 'app/Partials/Facility/facilityBookingAdd.html'
                })
                .when('/service/', {
                    controller: 'serviceController',
                    templateUrl: 'app/Partials/Service/service.html'
                })
                .when('/addservice/:service_id?', {
                    controller: 'serviceController',
                    templateUrl: 'app/Partials/Service/serviceAdd.html'
                })
                .when('/unit/', {
                    controller: 'unitController',
                    templateUrl: 'app/Partials/Unit/unit.html'
                })
                .when('/addunit/:unit_id?', {
                    controller: 'unitController',
                    templateUrl: 'app/Partials/Unit/unitAdd.html'
                })
                .when('/apartment/', {
                    controller: 'apartmentController',
                    templateUrl: 'app/Partials/Apartment/apartment.html'
                })
                .when('/addapartment/:apartment_id?', {
                    controller: 'apartmentController',
                    templateUrl: 'app/Partials/Apartment/apartmentAdd.html'
                })
                .when('/tenant/', {
                    controller: 'tenantController',
                    templateUrl: 'app/Partials/Tenant/tenant.html'
                })
                .when('/addtenant/:tenant_id?', {
                    controller: 'tenantController',
                    templateUrl: 'app/Partials/Tenant/tenantAdd.html'
                })
                .when('/lease/', {
                    controller: 'leaseController',
                    templateUrl: 'app/Partials/Lease/lease.html'
                })
                .when('/addlease/:lease_id?', {
                    controller: 'leaseController',
                    templateUrl: 'app/Partials/Lease/leaseAdd.html'
                })
                .when('/tenantlease/', {
                    controller: 'leaseController',
                    templateUrl: 'app/Partials/Lease/tenantLease.html'
                })
                .when('/employee/', {
                    controller: 'employeeController',
                    templateUrl: 'app/Partials/Employee/employee.html'
                })
                .when('/addemployee/:employee_id?', {
                    controller: 'employeeController',
                    templateUrl: 'app/Partials/Employee/employeeAdd.html'
                })
                .when('/workorderrequest/', {
                    controller: 'workOrderController',
                    templateUrl: 'app/Partials/WorkOrder/workorderrequest.html'
                })
                .when('/workorderrequestmanager/', {
                    controller: 'workOrderManagerController',
                    templateUrl: 'app/Partials/WorkOrder/workorderrequestmanager.html'
                })
                .when('/addworkorderrequest/:workrequest_id?', {
                    controller: 'workOrderController',
                    templateUrl: 'app/Partials/WorkOrder/workorderrequestAdd.html'
                })
                .when('/editworkorderrequest/:workrequestmanager_id?', {
                    controller: 'workOrderManagerController',
                    templateUrl: 'app/Partials/WorkOrder/workordermanagerEdit.html'
                })
                 .when('/inventory/', {
                     controller: 'inventoryController',
                     templateUrl: 'app/Partials/Inventory/inventory.html'
                 })
                .when('/addinventory/:inventory_id?', {
                    controller: 'inventoryController',
                    templateUrl: 'app/Partials/Inventory/inventoryAdd.html'
                })
                  .when('/servicerequest/', {
                      controller: 'serviceRequestController',
                      templateUrl: 'app/Partials/Service/servicerequest.html'
                  })
                .when('/addservicerequest/:servicerequest_id?', {
                    controller: 'serviceRequestController',
                    templateUrl: 'app/Partials/Service/servicerequestAdd.html'
                })
                .when('/unitphoto/', {
                    controller: 'unitphotoController',
                    templateUrl: 'app/Partials/Unit/unitPhoto.html'
                })
                .when('/manageuser/', {
                    controller: 'userController',
                    templateUrl: 'app/Partials/Login/userPage.html'
                })
                .when('/occupant/', {
                    controller: 'occupantController',
                    templateUrl: 'app/Partials/Occupant/occupant.html'
                })
                .when('/addoccupant/:occupant_id?', {
                    controller: 'occupantController',
                    templateUrl: 'app/Partials/Occupant/occupantAdd.html'
                })
                .otherwise({ redirectTo: '/index' });
            $animateProvider.classNameFilter(/details-row/);

        }]).run(function ($rootScope, $location) {
                $rootScope.$on('$locationChangeSuccess', function () {
                    $rootScope.actualLocation = $location.path();
                });
                $rootScope.$watch(function () { return $location.path() }, function (newLocation, oldLocation) {
                    if ($rootScope.actualLocation === newLocation) {
                        var accessToken = sessionStorage.getItem('accessToken');
                        if (accessToken == null) {
                            $location.path("/login");
                        }
                    }
                });
           });
            
      
