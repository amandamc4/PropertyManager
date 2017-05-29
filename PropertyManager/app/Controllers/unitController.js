
angular.module("propertyManagerApp").controller("unitController", ["$scope", "$filter", '$location', "$routeParams", "unitService", "unitphotoService", "userProfile", unitController]);

function unitController($scope, $filter, $location, $routeParams, unitService, unitphotoService, userProfile) {

    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;

    $scope.unitPhotos = [];
    $scope.files = [];
    $scope.file = "";

    if ($routeParams.unit_id) {
        $scope.editId = $routeParams.unit_id;
        $scope.isEdit = true;
        getUnitById($scope.editId);
    }
    else {
        getUnit();
    }


    $scope.modelAdd = {
        unitId: "",
        bedrooms: "",
        bathrooms: "",
        squareFt: "",
        maxOccupants: "",
        balcony: false,
        dishwasher: false,
        laundry: false
    };

    $scope.modelEdit = {
        unitId: "",
        bedrooms: "",
        bathrooms: "",
        squareFt: "",
        maxOccupants: "",
        balcony: "",
        dishwasher: "",
        laundry: "",
        PathName: "",
        UnitPhotoId: ""
    };

    $scope.photomodel = {
        UnitId: $scope.modelAdd.unitId,
        Description: $scope.description,
        PathName: ""
    };

    $scope.photomodelEdit = {
        Id: "",
        UnitId: $scope.modelAdd.unitId,
        Description: $scope.description,
        PathName: ""
    };

    $scope.message = "";
    $scope.units = [];
    $scope.sortType = "bedrooms";
    $scope.sortReverse = false;
    $scope.searchUnit = "";


    // ADD SECTION 

    $scope.addOneClick = function () {
        $location.path('/addunit');
    }
    
    // ADD UNIT AND THEN ADD PICTURE IF USER ATTACHED A IMAGE
    $scope.addUnit = function () {

        var unit = {
            Bedrooms: $scope.modelAdd.bedrooms,
            Bathrooms: $scope.modelAdd.bathrooms,
            SquareFeet: $scope.modelAdd.squareFt,
            MaxOccupants: $scope.modelAdd.maxOccupants,
            Balcony: $scope.modelAdd.balcony,
            Dishwasher: $scope.modelAdd.dishwasher,
            Laundry: $scope.modelAdd.laundry
        };

        var addResults = unitService.addUnit(unit);
        addResults.then(function (response) {
            $scope.modelAdd.unitId = response.data.Id;
            $scope.showConfirmation = true;
            $scope.modelAdd.balcony = response.data.Balcony;
            $scope.modelAdd.dishwasher = response.data.Dishwasher;
            $scope.modelAdd.laundry = response.data.Laundry;
            $scope.message = "Unit Added"
            return response;
        }).then(function (response) {
            if ($scope.file != "") {
                $scope.photomodel.UnitId = $scope.modelAdd.unitId;
                $scope.photomodel.Description = $scope.description;
                $scope.photomodel.PathName = "";

                var photo = unitphotoService.addUnitPhoto($scope.photomodel, $scope.file);
                photo.then(function (response) {
                    $scope.photomodel.PathName = "/Album/FileUploads/" + $scope.file.name;
                }, function (error) {
                    $scope.message = error.statusText + " " + error.status;
                })
            }           
        }, function (error) {
            $scope.message = error.statusText + " " + error.status;
        });

    } // close function

    $scope.addAnother = function () {
        $scope.modelAdd = {
            unitId: "",
            bedrooms: "",
            bathrooms: "",
            squareFt: "",
            maxOccupants: "",
            balcony: false,
            dishwasher: false,
            laundry: false
        };
        $scope.files = [];
        $scope.file = "";
        $scope.photomodel.PathName = "";
        $scope.photomodel.UnitId = "";
        $scope.message = "";
        $scope.form.$setPristine();
        $scope.showConfirmation = false;
    }

    // GET SECTION ***********************************************************************

    function getUnit () {
        var allResults = unitService.getAllUnit();
        allResults.then(function (response) {
            $scope.units = response.data;
        }, function (error) {
            $scope.message = response.statusText;
        })

    } // close function

    function getUnitById (id) {
        var resultById = unitService.getByIdUnit(id);
        resultById.then(function (response) {
            $scope.modelEdit.unitId = response.data.Id;
            $scope.modelEdit.bedrooms = response.data.Bedrooms;
            $scope.modelEdit.bathrooms = response.data.Bathrooms;
            $scope.modelEdit.squareFt = response.data.SquareFeet;
            $scope.modelEdit.maxOccupants = response.data.MaxOccupants;
            $scope.modelEdit.balcony = response.data.Balcony;
            $scope.modelEdit.dishwasher = response.data.Dishwasher;
            $scope.modelEdit.laundry = response.data.Laundry;

            if (response.data.UnitPhotos.length > 0) {
                $scope.modelEdit.PathName = response.data.UnitPhotos[0].PathName;
                $scope.modelEdit.UnitPhotoId = response.data.UnitPhotos[0].Id;
                $scope.photomodelEdit.PathName = response.data.UnitPhotos[0].PathName;
            }
        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    // *********** EDIT SECTION ******************************************

    $scope.editClick = function (id) {
        $location.path('/addunit/' + id);
    }

    $scope.editUnit = function () {

        // EDIT UNIT AND EDIT PICTURE IF USER CHOSE SO
        var unit = {
            Id: $scope.editId,
            Bedrooms: $scope.modelEdit.bedrooms,
            Bathrooms: $scope.modelEdit.bathrooms,
            SquareFeet: $scope.modelEdit.squareFt,
            MaxOccupants: $scope.modelEdit.maxOccupants,
            Balcony: $scope.modelEdit.balcony,
            Dishwasher: $scope.modelEdit.dishwasher,
            Laundry: $scope.modelEdit.laundry
        };

        var editResults = unitService.editUnit(unit, unit.Id);
        editResults.then(function (response) {
            $scope.message = "Edit successful";
            $scope.showEditConfirmation = true;
            $scope.modelEdit.balcony = response.data.Balcony;
            $scope.modelEdit.laundry = response.data.Laundry;
            $scope.modelEdit.dishwasher = response.data.Dishwasher;

            return response;
        }).then(function (response) {
            if ($scope.modelEdit.UnitPhotoId != "" && $scope.file != "") {
                $scope.photomodelEdit.Id = $scope.modelEdit.UnitPhotoId;
                $scope.photomodelEdit.UnitId = $scope.modelEdit.unitId;
                $scope.photomodelEdit.Description = $scope.description;
                $scope.photomodelEdit.PathName = "";
          
                var photo = unitphotoService.editUnitPhoto($scope.photomodelEdit, $scope.file);
                photo.then(function (response) {
                    $scope.photomodelEdit.PathName = "/Album/FileUploads/" + $scope.file.name;
                }, function (error) {
                    $scope.message = error.statusText + " " + error.status;
                })
            }
            else {
                if ($scope.file != "") {
                    addUnitPhotoInEdit();
                }               
            }
            
        }, function (error) {
            $scope.message = error.statusText + " " + error.status;
        });
    }  

    $scope.sorterFunc = function (unit) {
        return parseInt(unit.Bedrooms);
    };

    //************** DELETE ************************
    $scope.delete = function (id) {
        var deleteOne = unitService.deleteUnit(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getUnit();
        }, function (error) {
            $scope.message = error.statusText;
        });
    }

    $scope.cancelAdd = function () {
        $location.path('/unit');
    }

    $scope.goBack = function () {
        $location.path('/unit');
    }

    // *************************************************** UNIT PHOTO *****************************************************

    // CHECK TO SEE IF USER ATTACHED IMAGE
    $scope.$on("seletedFile", function (event, args) {
        $scope.$apply(function () {
            //$scope.files.push(args.file);
            $scope.file = args.file;
        });
    });

    // ADD IMAGE WHEN USER IS IN EDIT UNIT MODE
    function addUnitPhotoInEdit() {
        $scope.photomodel.UnitId = $scope.modelEdit.unitId;
        $scope.photomodel.Description = "";
        $scope.photomodel.PathName = "";
       
        var photo = unitphotoService.addUnitPhoto($scope.photomodel, $scope.file);
        photo.then(function (response) {
            $scope.photomodelEdit.PathName = "/Album/FileUploads/" + $scope.file.name;
        }, function (error) {
        });
    };

    // GET UNIT PHOTO BY ID
    function getUnitPhotoById (id) {
        var resultById = unitphotoService.getByIdUnitPhoto(id);
        resultById.then(function (response) {
            $scope.imageUrl = response.data.PathName;
            $scope.description = response.data.Description;
            $scope.unitId = response.data.UnitId;
        }, function (error) {
            $scope.message = response.statusText;
        })
    } // close function

    // DELETE IMAGE
    $scope.deletePicture = function (unitPhotoId) {
        var deleteOne = unitphotoService.deleteUnitPhoto(unitPhotoId);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            $scope.modelEdit.PathName = "";
            $scope.modelEdit.UnitPhotoId = "";
            $scope.photomodel.PathName = "";
            $scope.photomodel.UnitId = "";
            $scope.photomodelEdit.PathName = "";
            $scope.photomodelEdit.UnitId = "";
        }, function (error) {
            $scope.errorMessage = "Could not delete";
        });
    }

    // OPEN IMAGE IN ORIGINAL SIZE
    $scope.openPic = function (pathName) {
        window.open(pathName);
    }

}