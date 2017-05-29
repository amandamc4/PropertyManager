
angular.module("propertyManagerApp").controller("inventoryController", ["$scope", "$filter", '$location', "$routeParams", "inventoryService", "userProfile", inventoryController]);

function inventoryController($scope, $filter, $location, $routeParams, inventoryService, userProfile) {

    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;

    if ($routeParams.inventory_id) {
        $scope.editId = $routeParams.inventory_id;
        $scope.isEdit = true;
        getInventoryById($scope.editId);
    }
    else {
        getInventory();
    }

    $scope.modelAdd = {
        inventoryId: "",
        supplier: "",
        quantity: 0,
        productName: "",
    };

    $scope.modelEdit = {
        inventoryId: "",
        supplier: "",
        quantity: 0,
        productName: "",
    };

    $scope.inventorys = [];
    $scope.message = "";
    $scope.sortType = "productName";
    $scope.sortReverse = false;
    $scope.searchInventory = "";

    // ADD SECTION 

    $scope.addOneClick = function () {
        $location.path('/addinventory');
    }
    

    // ********* ADD SECTION
    $scope.addInventory = function () {

        var inventory = {
            ProductName: $scope.modelAdd.productName,
            Supplier: $scope.modelAdd.supplier,
            Quantity: $scope.modelAdd.quantity,
        };

        var addResults = inventoryService.addInventory(inventory);
        addResults.then(function (response) {
            $scope.modelAdd.inventoryId = response.data.Id;
            $scope.showConfirmation = true;
            $scope.message = "Inventory Added"
        }, function (error) {
            $scope.message = error.statusText + " " + error.status;
        });
    } // close function

    $scope.addAnother = function () {
        $scope.modelAdd = {
            inventoryId: "",
            supplier: "",
            quantity: 0,
            productName: "",
        };
        $scope.message = "";
        $scope.form.$setPristine();
        $scope.showConfirmation = false;
    }

    //*********** GET ALL
    function getInventory () {
        var allResults = inventoryService.getAllInventory();
        allResults.then(function (response) {
            $scope.inventorys = response.data;
        }, function (error) {
            $scope.message = response.statusText;
        })

    } // close function

    function getInventoryById (id) {
        var resultById = inventoryService.getByIdInventory(id);
        resultById.then(function (response) {
            $scope.modelEdit.productName = response.data.ProductName;
            $scope.modelEdit.supplier = response.data.Supplier;
            $scope.modelEdit.quantity = response.data.Quantity;
         }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    // *********** EDIT SECTION ******************************************

    $scope.editClick = function (id) {
        $location.path('/addinventory/' + id);
    }

    $scope.editInventory = function () {

        var inventory = {
            Id: $scope.editId,
            ProductName: $scope.modelEdit.productName,
            Supplier: $scope.modelEdit.supplier,
            Quantity: $scope.modelEdit.quantity
        };

        var editResults = inventoryService.editInventory(inventory, inventory.Id);
        editResults.then(function (response) {
            $scope.message = "Edit successful";
            $scope.showEditConfirmation = true;
        }, function (error) {
            $scope.message = error.statusText;
        });
    } // close function


    //************** DELETE ************************
    $scope.delete = function (id) {
        var deleteOne = inventoryService.deleteInventory(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getInventory();
        }, function (error) {
            $scope.message = error.statusText;
        });
    }

    $scope.cancelAdd = function () {
        $location.path('/inventory');
    }

    $scope.goBack = function () {
        $location.path('/inventory');
    }


}