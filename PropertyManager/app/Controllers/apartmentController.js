
angular.module("propertyManagerApp").controller("apartmentController", ["$scope", "$filter", '$location', "$routeParams", "apartmentService", "unitService", "userProfile", apartmentController]);

function apartmentController($scope, $filter, $location, $routeParams, apartmentService, unitService, userProfile) {
   
    $scope.unitType = "";
    $scope.message = "";
    $scope.errorMessage = "";
    $scope.apartments = [];
    $scope.units = [];
    $scope.sortType = "ApartmentNumber";
    $scope.sortReverse = false;
    $scope.searchApartment = "";

    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;
    $scope.occupied = "";
    $scope.vaccant = "";
    $scope.colors = ["rgb(255,215,0)", "rgb(51,122,183)"];

    if ($routeParams.apartment_id) {
        $scope.editId = $routeParams.apartment_id;
        $scope.isEdit = true;
        getApartmentById($scope.editId);
    }     
    else{
        getApartment();
    }

    getUnits();

    $scope.modelAdd = {
        aptNumber: "",
        floorNumber: "",
        status: "Vacant",
        unitId: "",
        unitType: 0
    };

    $scope.modelEdit = {
        aptNumber: 0,
        floorNumber: 0,
        status: "",
        unitId: "",
        unitType: 0
    };

    $scope.addOneClick = function () {
        $location.path('/addapartment');
    }

    // ******************************** ADD SECTION

    $scope.addApartment = function () {    
        $scope.errorMessage = "";
        var apartment = {
            ApartmentNumber: $scope.modelAdd.aptNumber,
            FloorNumber: $scope.modelAdd.floorNumber,
            Status: $scope.modelAdd.status,
            UnitId: $scope.modelAdd.unitId,
        };

        var addResults = apartmentService.addApartment(apartment);
        addResults.then(function (response) {
            $scope.modelAdd.aptNumber = response.data.ApartmentNumber;
            $scope.modelAdd.unitType = response.data.Unit.Bedrooms
            $scope.showConfirmation = true;
            $scope.message="Apartment Added"
        }, function (error) {        
           $scope.errorMessage = "This apartment already exists in the Database";                     
        });

     } // close function

     $scope.addAnother = function () {
         $scope.modelAdd = {
             aptNumber: 0,
             floorNumber: 0,
             status: "",
             unitId: 0,
             unitType: 0
         };
         $scope.message = "";
         $scope.form.$setPristine();
         $scope.showConfirmation = false;
     }

    //******************************************************************************************//

    //GET ALL
    function getApartment() {
        var allResults = apartmentService.getAllApartment();
        allResults.then(function (response) {
            $scope.apartments = response.data;
            angular.forEach($scope.apartments, function (value) {
                if (value.Status == "Occupied") {
                    $scope.occupied++;
                }
                else {
                    $scope.vaccant++;
                }
            });
            $scope.labels = ["Vacant", "Occupied"];
            $scope.data = [$scope.vaccant, $scope.occupied];
        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    function getApartmentById (id) {
        var resultById = apartmentService.getByIdApartment(id);
        resultById.then(function (response) {
            $scope.modelEdit.aptNumber = response.data.ApartmentNumber;
            $scope.modelEdit.floorNumber = response.data.FloorNumber;
            $scope.modelEdit.status = response.data.Status;
            $scope.modelEdit.unitId = response.data.UnitId;
            $scope.modelEdit.unitType = response.data.Unit.Bedrooms;
        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    // *********** EDIT SECTION ******************************************

    $scope.editClick = function (id) {
        $location.path('/addapartment/' + id);
    }

    $scope.editApartment = function () {

        var apartment = {
            ApartmentNumber: $scope.modelEdit.aptNumber,
            FloorNumber: $scope.modelEdit.floorNumber,
            Status: $scope.modelEdit.status,
            UnitId: $scope.modelEdit.unitId,
        };

        var editResults = apartmentService.editApartment(apartment, $scope.modelEdit.aptNumber);
        editResults.then(function (response) {
            $scope.message = "Edit successful";
            $scope.modelEdit.unitType = response.data.Unit.Bedrooms;
            $scope.showEditConfirmation = true;      
        }, function (error) {
            $scope.message = error.statusText;      
        });
    } // close function

    //************** DELETE ************************
    $scope.delete = function (id) {
        $scope.errorMessage = "";
        var deleteOne = apartmentService.deleteApartment(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getApartment();
        }, function (error) {
            if (error.data == "Lease associated") {
                $scope.errorMessage = "There's a lease associated with this apartment.\nPlease delete the lease first.";
            }            
        });
    }

    $scope.cancelAdd = function () {
        $location.path('/apartment');
    }

    $scope.goBack = function () {
        $location.path('/apartment');
    }

    // GET UNITS FOR DROPDOWN MENU
    function getUnits() {
        var allResults = unitService.getAllUnit();
        allResults.then(function (response) {
            $scope.units = response.data;
        }, function (error) {
            $scope.message = error.statusText;
        })
    }

    // DOUGHNUT CHART
    $scope.options = {
        maintainAspectRatio: false,
        legend: {
            display: false,
            labels: {
                fontColor: 'rgb(255, 99, 132)',
                position: 'right'
            }
        },
        tooltip: {
            position: 'nearest'
        }
    };
    

}