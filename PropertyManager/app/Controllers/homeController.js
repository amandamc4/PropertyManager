
angular.module("propertyManagerApp").controller("homeController", ["$scope", 'uiCalendarConfig', "facilityBookingService", "serviceRequestService", "workOrderService", "userProfile", homeController]);

function homeController($scope, uiCalendarConfig, facilityBookingService, serviceRequestService, workOrderService, userProfile) {


    var user = userProfile.getProfile();
    
    $scope.isLoggedIn = user.isLoggedIn;
    $scope.firstName = user.firstName;
    $scope.userRole = user.userRole;
    $scope.date = new Date();

    $scope.SelectedEvent = null;
    var isFirstTime = true;
    $scope.events = [];
    $scope.eventSources = [$scope.events];

    getFacilityBooking();

    // GETS DATA FROM FACILITY BOOKING, WORK ORDER AND SERVICE REQUESTS TO DISPLAY IN THE CALENDAR
    function getFacilityBooking() {
        var allFacilityBookings = facilityBookingService.getAllFacilityBooking();
        allFacilityBookings.then(function (response) {
            $scope.facilityBookings = response.data;
            $scope.events.slice(0, $scope.events.length);
            angular.forEach($scope.facilityBookings, function (value) {
                $scope.events.push({
                    title: value.Facility.FacilityName,
                    description: value.Notes,
                    start: new Date(value.StartTime.replace('T', ' ').replace('-', '/')),
                    end: new Date(value.EndTime.replace('T', ' ').replace('-', '/')),
                    stick: true
                });
            });
        }, function (error) {
            $scope.message = error.statusText;
        }).then(function () {
            var allServiceRequests = serviceRequestService.getAllServiceRequest();
            allServiceRequests.then(function (response) {
                $scope.serviceRequests = response.data;
                angular.forEach($scope.serviceRequests, function (value) {
                    var completionDate = "";
                    if (value.CompletionDate != null) {
                        completionDate = new Date(value.CompletionDate.replace('T', ' ').replace('-', '/'));
                    }
                    $scope.events.push({
                        title: value.Service.ServiceName,
                        description: value.Notes,
                        start: new Date(value.RequestDate.replace('T', ' ').replace('-', '/')),
                        end: completionDate,
                        stick: true
                    });
                });
            }, function (error) {
                $scope.message = error.statusText;
            })
        }).then(function () {
            var allRequests = workOrderService.getAllWorkOrder();
            allRequests.then(function (response) {
                $scope.workOrders = response.data;
                angular.forEach($scope.workOrders, function (value) {
                    var completionDate = "";
                    if (value.CompletionDate != null) {
                        completionDate = new Date(value.CompletionDate.replace('T', ' ').replace('-', '/'));
                    }
                    $scope.events.push({
                        title: value.Description,
                        description: value.Notes,
                        start: new Date(value.RequestDate.replace('T', ' ').replace('-', '/')),
                        end: "",
                        stick: true
                    });
                });
            }, function (error) {
                $scope.message = error.statusText;
            })
        })
    } // close function


    // CALENDAR CONFIGURATION
    $scope.uiConfig = {
        calendar: {
            height: 450,
            editable: false,
            displayEventTime: false,
            header: {
                left: 'month agendaWeek',
                center: 'title',
                right: 'today prev,next'
            },
            eventClick: function (event) {
                $scope.SelectedEvent = event;
            },
            eventAfterAllRender: function () {
                if ($scope.events.length > 0 && isFirstTime) {
                    uiCalendarConfig.calendars.myCalendar.fullCalendar('gotoDate', $scope.events[0].start);
                    isFirstTime = false;
                }
            }
        }
    };
    
}