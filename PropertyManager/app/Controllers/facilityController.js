
angular.module("propertyManagerApp").controller("facilityController", ["$scope", "$filter", '$location', "$routeParams", "facilityService", "userProfile", facilityController]);

function facilityController($scope, $filter, $location, $routeParams, facilityService, userProfile) {
   
    $scope.message = "";
    $scope.facilities = [];
    $scope.sortType = "title";
    $scope.sortReverse = false;
    $scope.searchFacility = "";

    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;

    if ($routeParams.facility_id) {
        $scope.editId = $routeParams.facility_id;
        $scope.isEdit = true;
        getFacilityById($scope.editId);
    }
    else {
        getFacility();
    }

    $scope.modelAdd = {
        facilityId: "",
        name: "",
        description: "",
        location: "",
        status: "",
        openTime: "",
        closeTime: ""

    };

    $scope.modelEdit = {
        facilityId: $scope.editId,
        name: "",
        description: "",
        location: "",
        status: "",
        openTime: "",
        closeTime: ""
    };

    $scope.addOneClick = function () {
        $location.path('/addfacility');
    }

    // ********** ADD SECTION
    $scope.addFacility = function () {

        $scope.message = "";
        var add = true;

        if ($scope.modelAdd.closeTime <= $scope.modelAdd.openTime) {
            $scope.message = "Close time must be greater than open time."
            add = false;
        }

        if (add == true) {
            var openTimeFiltered = null;
            var expireDateFiltered = null;

            if ($scope.modelAdd.openTime != "") {
                openTimeFiltered = $filter('date')($scope.modelAdd.openTime, 'HH:mm');
            }
            if ($scope.modelAdd.closeTime != "") {
                expireDateFiltered = $filter('date')($scope.modelAdd.closeTime, 'HH:mm');
            }

            var facility = {
                FacilityName: $scope.modelAdd.name,
                Description: $scope.modelAdd.description,
                Location: $scope.modelAdd.location,
                Status: $scope.modelAdd.status,
                OpenTime: openTimeFiltered,
                CloseTime: expireDateFiltered
            };

            var addResults = facilityService.addFacility(facility);
            addResults.then(function (response) {
                $scope.modelAdd.facilityId = response.data.Id;
                $scope.showConfirmation = true;
                $scope.message = "Facility Added"
            }, function (error) {
                $scope.message = error.statusText + " " + error.status;
            });
        }

    } // close function

    // **************** GET ALL
    function getFacility() {
        var allResults = facilityService.getAllFacility();
        allResults.then(function (response) {
            $scope.facilities = response.data;
        }, function (error) {
            $scope.message = error.statusText;
        })
    } // close function

    $scope.addAnother = function () {
        $scope.modelAdd = {
            facilityId: "",
            name: "",
            description: "",
            location: "",
            status: "",
            openTime: "",
            closeTime: ""
        };
        $scope.message = "";
        $scope.form.$setPristine();
        $scope.showConfirmation = false;
    }

    function getFacilityById (id) {
        var resultById = facilityService.getByIdFacility(id);
        resultById.then(function (response) {
            $scope.modelEdit.name = response.data.FacilityName;
            $scope.modelEdit.description = response.data.Description;
            $scope.modelEdit.location = response.data.Location;
            $scope.modelEdit.status = response.data.Status;
            if (response.data.OpenTime != null) {
                $scope.modelEdit.openTime = new Date(response.data.OpenTime.replace('T', ' ').replace('-', '/'));
            }
            if (response.data.CloseTime != null) {
                $scope.modelEdit.closeTime = new Date(response.data.CloseTime.replace('T', ' ').replace('-', '/'));
            }           
        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    // *********** EDIT SECTION ******************************************

    $scope.editClick = function (id) {
        $location.path('/addfacility/' + id);
    }

    $scope.editFacility = function () {

        $scope.message = "";
        var add = true;

        if ($scope.modelEdit.closeTime <= $scope.modelEdit.openTime) {
            $scope.message = "Close time must be greater than open time."
            add = false;
        }

        if (add == true) {
            var openTimeFiltered = null;
            var expireDateFiltered = null;

            if ($scope.modelEdit.openTime != "") {
                var openTimeFiltered = $filter('date')($scope.modelEdit.openTime, 'HH:mm:ss');
            }

            if ($scope.modelEdit.closeTime != "") {
                var expireDateFiltered = $filter('date')($scope.modelEdit.closeTime, 'HH:mm:ss');
            }

            var facility = {
                Id: $scope.editId,
                FacilityName: $scope.modelEdit.name,
                Description: $scope.modelEdit.description,
                Location: $scope.modelEdit.location,
                Status: $scope.modelEdit.status,
                OpenTime: openTimeFiltered,
                CloseTime: expireDateFiltered
            };

            var editResults = facilityService.editFacility(facility, facility.Id);
            editResults.then(function (response) {
                $scope.message = "Edit successful";
                $scope.showEditConfirmation = true;
            }, function (error) {
                $scope.message = error.statusText;
            });
        }
    } // close function

    //************** DELETE ************************
    $scope.delete = function (id) {
        var deleteOne = facilityService.deleteFacility(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getFacility();
        }, function (error) {
            $scope.message = error.statusText;
        });
    }

    $scope.cancelAdd = function () {
        $location.path('/facility');
    }

    $scope.goBack = function () {
        $location.path('/facility');
    }
   
    //TIME PICKER
    $scope.hstep = 1;
    $scope.mstep = 1;

    $scope.ismeridian = true;
    $scope.toggleMode = function () {
        $scope.ismeridian = !$scope.ismeridian;
    };

}