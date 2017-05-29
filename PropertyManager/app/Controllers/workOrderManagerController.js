
angular.module("propertyManagerApp").controller("workOrderManagerController", ["$scope", "$filter", '$location', "$routeParams", "workOrderService", "userProfile", "tenantService", workOrderManagerController]);

function workOrderManagerController($scope, $filter, $location, $routeParams, workOrderService, userProfile, tenantService) {

    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;

    if ($routeParams.workrequestmanager_id) {
        $scope.editId = $routeParams.workrequestmanager_id;
        $scope.isEdit = true;
        getWorkOrderById($scope.editId);
    }
    else {
        getWorkOrder();
    }

    var user = userProfile.getProfile();
    $scope.userRole = user.userRole;

    $scope.modelEdit = {
        workOrderId: "",
        description: "",
        notes: "",
        requestDate: "",
        completionDate: "",
        tenantId: ""
    };

    $scope.workOrders = [];
    $scope.tenantId = "";
    $scope.message = "";
    $scope.sortType = "description";
    $scope.sortReverse = false;
    $scope.searchWorkOrder = "";
    $scope.activeWorkOrders = "";

    var today = new Date();
    today.setHours(0, 0, 0, 0);
    $scope.today = today;

    // GET ALL WORK ORDERS FROM ALL TENANT FOR MANAGEMENT VIEW
    function getWorkOrder() {
        $scope.workOrders = [];
        var allRequests = workOrderService.getAllWorkOrder();
        allRequests.then(function (response) {
            $scope.workOrders = response.data;
            angular.forEach($scope.workOrders, function (value) {
                if (value.CompletionDate == null) {
                    $scope.activeWorkOrders++;
                }
            });
        }, function (error) {
            $scope.message = error.statusText;
        })

    }; // close function

    // GET SPECIFIC WORK ORDER FOR EDITING
    function getWorkOrderById(id) {
        var RequestById = workOrderService.getByIdWorkOrder(id);
        RequestById.then(function (response) {
            $scope.modelEdit.description = response.data.Description;
            $scope.modelEdit.notes = response.data.Notes;
            $scope.modelEdit.tenantId = response.data.TenantId;
            $scope.modelEdit.workOrderId = response.data.Id;
            if (response.data.RequestDate != null) {
                $scope.modelEdit.requestDate = new Date(response.data.RequestDate.replace('T', ' ').replace('-', '/'));
            }
            if (response.data.CompletionDate != null) {
                $scope.modelEdit.completionDate = new Date(response.data.CompletionDate.replace('T', ' ').replace('-', '/'));
            }

        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    // *********** EDIT SECTION ******************************************

    $scope.editClick = function (id) {
        $location.path('/editworkorderrequest/' + id);
    }

    // EDIT COMPLETION DATE
    $scope.editWorkOrder = function () {

        $scope.message = "";
        var add = false;

        if ($scope.modelEdit.completionDate < $scope.today || $scope.modelEdit.completionDate < $scope.modelEdit.requestDate) {
            $scope.message = "Enter a date greater than today";
        }
        else {
            add = true;
        }

        if (add == true) {
            var requestDateFiltered = null;
            var completionDateFiltered = null;

            if ($scope.modelEdit.requestDate != "") {
                requestDateFiltered = $filter('date')($scope.modelEdit.requestDate, "yyyy-MM-dd");
            }
            if ($scope.modelEdit.completionDate != "") {
                completionDateFiltered = $filter('date')($scope.modelEdit.completionDate, "yyyy-MM-dd");
            }

            var workOrder = {
                Id: $scope.editId,
                Description: $scope.modelEdit.description,
                Notes: $scope.modelEdit.notes,
                RequestDate: requestDateFiltered,
                CompletionDate: completionDateFiltered,
            };

            var editResults = workOrderService.editWorkOrder(workOrder, workOrder.Id);
            editResults.then(function (response) {
                $scope.message = "Edit successful";
                $scope.showEditConfirmation = true;
            }, function (error) {
                $scope.message = error.statusText;
            });
        }
        
    } // close function

    // ********* DELETE 

    $scope.delete = function (id) {
        var deleteOne = workOrderService.deleteWorkOrder(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getWorkOrder();
        }, function (error) {
            $scope.message = error.statusText;
        });
    }
   

    $scope.cancelAdd = function () {
        $location.path('/workorderrequestmanager');
    }

    $scope.goBack = function () {
        $location.path('/workorderrequestmanager');
    }

    // DATE PICKER
    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate', 'MM/dd/yyyy'];
    $scope.format = $scope.formats[4];
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };
   
    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };


}