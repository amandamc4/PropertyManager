
angular.module("propertyManagerApp").controller("facilityBookingController", ["$scope", "$filter", '$location', "$routeParams", 'uiCalendarConfig', "facilityBookingService", "facilityService", "tenantService", "userProfile", facilityBookingController]);

function facilityBookingController($scope, $filter, $location, $routeParams, uiCalendarConfig, facilityBookingService, facilityService, tenantService, userProfile) {

    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;

    var user = userProfile.getProfile();
    $scope.userName = user.username;
    $scope.userRole = user.userRole;

    if ($routeParams.facilitybook_id) {
        $scope.editId = $routeParams.facilitybook_id;
        $scope.isEdit = true;
        getFacilityBookingById($scope.editId);
    }
    else {
        getFacilityBooking();
    }

    $scope.sortType = "name";
    $scope.sortReverse = false;
    $scope.searchBooking = "";
    $scope.facilityBookings = [];
    $scope.facilities = [];
    $scope.message = "";
    $scope.tenantId = "";

    getAllFacilities();

    if ($scope.userRole == 'Tenant') {
        getUserId();
    }
    
    $scope.modelAdd = {
        facilityBookId: "",
        facilityId: "",
        startTime: "",
        endTime: "",
        bookedDate: "",
        notes: "",
        tenantId: "",
        facilityName: "",
        tenantName: ""
    };

    $scope.modelEdit = {
        facilityBookId: "",
        facilityId: "",
        startTime: "",
        endTime: "",
        bookedDate: "",
        notes: "",
        tenantId: "",
        facilityName: "",
        tenantName: ""
    };

    var today = new Date();
    today.setHours(0, 0, 0, 0);
    $scope.today = today;

    $scope.addOneClick = function () {
        $location.path('/addbookfacility');
    }
   
    $scope.SelectedEvent = null;
    var isFirstTime = true;
    $scope.events = [];
    $scope.eventSources = [$scope.events];


    // ADD SECTION

    $scope.addFacilityBooking = function () {

        $scope.message = "";
        var add = true;

        // VALIDATE BOOKING DATE
        if ($scope.modelAdd.bookedDate <= $scope.today) {
            $scope.message = "Enter a date greater than today";
            add = false;
        }
        else {
            if ($scope.modelAdd.endTime < $scope.modelAdd.startTime) {
                $scope.message = "Enter a time greater than Start Time";
                add = false;
            }
        }
                
        if (add == true) {

            var bookedDateFiltered = null;
            var startTimeFiltered = null;
            var endDateFiltered = null;

            if ($scope.modelAdd.bookedDate != "") {
                bookedDateFiltered = $filter('date')($scope.modelAdd.bookedDate, "yyyy-MM-dd");
            }

            if ($scope.modelAdd.startTime != "") {
                startTimeFiltered = $filter('date')($scope.modelAdd.startTime, 'HH:mm');
            }
            if ($scope.modelAdd.endTime != "") {
                endDateFiltered = $filter('date')($scope.modelAdd.endTime, 'HH:mm');
            }

            var facilityBooking = {
                StartTime: startTimeFiltered,
                EndTime: endDateFiltered,
                Notes: $scope.modelAdd.notes,
                BookedDate: bookedDateFiltered,
                TenantId: $scope.tenantId,
                FacilityId: $scope.modelAdd.facilityId
            };

            var addResults = facilityBookingService.addFacilityBooking(facilityBooking);
            addResults.then(function (response) {
                $scope.modelAdd.facilityBookId = response.data.Id;
                $scope.modelAdd.facilityId = response.data.FacilityId;
                $scope.modelAdd.facilityName = response.data.Facility.FacilityName;
                $scope.modelAdd.tenantName = response.data.Tenant.FirstName + ' ' + response.data.Tenant.LastName;
                $scope.showConfirmation = true;
                $scope.message = "Facility Booked"
            }, function (error) {
                $scope.message = error.data;
            });
        }
        
    } // close function

    // GET SECTION

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
        })

   } // close function   

    $scope.addAnother = function () {
        $scope.modelAdd = {
            facilityBookId: "",
            facilityId: "",
            startTime: "",
            endTime: "",
            bookedDate: "",
            notes: "",
            tenantId: "",
            facilityName: "",
            tenantName: ""
        };
        $scope.message = "";
        $scope.form.$setPristine();
        $scope.showConfirmation = false;
    }

    function getFacilityBookingById(id) {
        var facilityBookingById = facilityBookingService.getByIdFacilityBooking(id);
        facilityBookingById.then(function (response) {
            $scope.modelEdit.notes = response.data.Notes;
            $scope.modelEdit.facilityBookId = response.data.Id;
            $scope.modelEdit.facilityId = response.data.FacilityId;
            $scope.modelEdit.tenantId = response.data.TenantId;
            $scope.modelEdit.facilityName = response.data.Facility.FacilityName;
            $scope.modelEdit.tenantName = response.data.Tenant.FirstName + ' ' + response.data.Tenant.LastName;
            if (response.data.BookedDate != null) {
                $scope.modelEdit.bookedDate = new Date(response.data.BookedDate.replace('T', ' ').replace('-', '/'));
            }
            if (response.data.StartTime != null) {
                $scope.modelEdit.startTime = new Date(response.data.StartTime.replace('T', ' ').replace('-', '/'));
            }
            if (response.data.EndTime != null) {
                $scope.modelEdit.endTime = new Date(response.data.EndTime.replace('T', ' ').replace('-', '/'));
            }
        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    // *********** EDIT SECTION ******************************************

    $scope.editClick = function (id) {
        $location.path('/addbookfacility/' + id);
    } 

    $scope.editFacilityBooking = function () {

        $scope.message = "";
        var add = true;

        // VALIDATE DATE
        if ($scope.modelEdit.bookedDate <= $scope.today) {
            $scope.message = "Enter a date greater than today";
            add = false;
        }
        else {
            if ($scope.modelEdit.endTime < $scope.modelEdit.startTime) {
                $scope.message = "Enter a time greater than Start Time";
                add = false;
            }
        }

        if (add == true) {

            var bookedDateFiltered = null;
            var startTimeFiltered = null;
            var endDateFiltered = null;

            if ($scope.modelEdit.bookedDate != "") {
                bookedDateFiltered = $filter('date')($scope.modelEdit.bookedDate, "yyyy-MM-dd");
            }

            if ($scope.modelEdit.startTime != "") {
                startTimeFiltered = $filter('date')($scope.modelEdit.startTime, 'yyyy-MM-dd HH:mm:ss');
            }
            if ($scope.modelEdit.endTime != "") {
                endDateFiltered = $filter('date')($scope.modelEdit.endTime, 'yyyy-MM-dd HH:mm:ss');
            }

            var facilityBooking = {
                Id: $scope.editId,
                StartTime: startTimeFiltered,
                EndTime: endDateFiltered,
                Notes: $scope.modelEdit.notes,
                BookedDate: bookedDateFiltered,
                TenantId: $scope.tenantId,
                FacilityId: $scope.modelEdit.facilityId
            };

            var editResults = facilityBookingService.editFacilityBooking(facilityBooking, facilityBooking.Id);
            editResults.then(function (response) {
                $scope.message = "Edit successful";
                $scope.showEditConfirmation = true;
            }, function (error) {
                $scope.message = error.data;
            });
        }
        
    } // close function

    // GET FACILITIES FOR DROPDOWN MENU
    function getAllFacilities() {
        var facility = facilityService.getAllFacility();
        facility.then(function (response) {
            $scope.facilities = response.data;
        }, function (error) {
            $scope.message = error.statusText;
        })
    }

    // GET USER ID TO ASSOCIATE BOOKING WITH USER
    function getUserId() {
        var user = tenantService.getByEmailTenant($scope.userName);
        user.then(function (response) {
            $scope.modelAdd.tenantId = response.data.Id;
            $scope.tenantId = response.data.Id;
        }, function (error) {
            $scope.message = error.statusText + " " + error.status;
        })
    }

    //************** DELETE ************************
    $scope.delete = function (id) {
        var deleteOne = facilityBookingService.deleteFacilityBooking(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getFacilityBooking();
        }, function (error) {
            $scope.message = error.statusText;
        });
    }

    $scope.cancelAdd = function () {
        $location.path('/bookfacility');
    }

    $scope.goBack = function () {
        $location.path('/bookfacility');
    }

    // CALENDAR ************************************************************

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

    //FILTER TO DISPLAY ONLY NON-EXPIRED BOOKINGS
    $scope.greaterThan = function (item) {
        return function (item) {
            var bookdate = new Date(item.BookedDate.replace('T', ' ').replace('-', '/'));
            return bookdate >= $scope.today;
        }
    }

    // DATE AND TIME PICKER
    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate', 'MM/dd/yyyy'];
    $scope.format = $scope.formats[4];
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.hstep = 1;
    $scope.mstep = 1;

    $scope.ismeridian = true;
    $scope.toggleMode = function () {
        $scope.ismeridian = !$scope.ismeridian;
    };

}