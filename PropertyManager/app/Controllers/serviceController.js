
angular.module("propertyManagerApp").controller("serviceController", ["$scope", "$filter", '$location', "$routeParams", "serviceService", "userProfile", serviceController]);

function serviceController($scope, $filter, $location, $routeParams, serviceService, userProfile) {

    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;

    if ($routeParams.service_id) {
        $scope.editId = $routeParams.service_id;
        $scope.isEdit = true;
        getServiceById($scope.editId);
    }
    else {
        getService();
    }


    $scope.modelAdd = {
        serviceId: "",
        name: "",
        companyName: "",
        phoneNumber: "",
        email: "",
        address: "",
    };

    $scope.modelEdit = {
        serviceId: "",
        name: "",
        companyName: "",
        phoneNumber: "",
        email: "",
        address: "",
    };

    $scope.message = "";
    $scope.services = [];
    $scope.sortType = "name";
    $scope.sortReverse = false;
    $scope.searchService = "";

    // ADD SECTION 

    $scope.addOneClick = function () {
        $location.path('/addservice');
    }

    $scope.addService = function() {

        var service = {
            ServiceName: $scope.modelAdd.name,
            CompanyName: $scope.modelAdd.companyName,
            PhoneNumber: $scope.modelAdd.phoneNumber,
            Email: $scope.modelAdd.email,
            Address: $scope.modelAdd.address
        };

        var addResults = serviceService.addService(service);
        addResults.then(function (response) {
            $scope.modelAdd.serviceId = response.data.Id;
            $scope.showConfirmation = true;
            $scope.message = "Service Added"
        }, function (error) {
            $scope.message = error.statusText + " " + error.status;
        });

    } // close function

    $scope.addAnother = function () {
        $scope.modelAdd = {
            serviceId: "",
            name: "",
            companyName: "",
            phoneNumber: "",
            email: "",
            address: "",
        };
        $scope.message = "";
        $scope.form.$setPristine();
        $scope.showConfirmation = false;
    }

    // ****** GET ALL

    function getService () {
        var allResults = serviceService.getAllService();
        allResults.then(function (response) {
            $scope.services = response.data;
        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    function getServiceById (id) {
        var resultById = serviceService.getByIdService(id);
        resultById.then(function (response) {
            $scope.modelEdit.serviceId = response.data.Id;
            $scope.modelEdit.name = response.data.ServiceName;
            $scope.modelEdit.companyName = response.data.CompanyName;
            $scope.modelEdit.phoneNumber = response.data.PhoneNumber;
            $scope.modelEdit.email = response.data.Email;
            $scope.modelEdit.address = response.data.Address;
          
        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    // *********** EDIT SECTION ******************************************

    $scope.editClick = function (id) {
        $location.path('/addservice/' + id);
    }

    $scope.editService = function () {

        var service = {
            Id: $scope.editId,
            ServiceName: $scope.modelEdit.name,
            CompanyName: $scope.modelEdit.companyName,
            PhoneNumber: $scope.modelEdit.phoneNumber,
            Email: $scope.modelEdit.email,
            Address: $scope.modelEdit.address
        };

        var editResults = serviceService.editService(service, service.Id);
        editResults.then(function (response) {
            $scope.message = "Edit successful";
            $scope.showEditConfirmation = true;
        }, function (error) {
            $scope.message = error.statusText;
        });
    } // close function

    //************** DELETE ************************
    $scope.delete = function (id) {
        var deleteOne = serviceService.deleteService(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getService();
        }, function (error) {
            $scope.message = error.statusText;
        });
    }

    $scope.cancelAdd = function () {
        $location.path('/service');
    }

    $scope.goBack = function () {
        $location.path('/service');
    }

}