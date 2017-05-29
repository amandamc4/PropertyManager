angular.module("propertyManagerApp").controller("unitphotoController", ["$http", "$scope", "$filter", "unitphotoService", "userProfile", unitphotoController]);

function unitphotoController($http, $scope, $filter,unitphotoService, userProfile) {
 
    $scope.name = "";
    $scope.description = "";
    $scope.unitId = "";
    $scope.message = "";
    $scope.unitPhotos = [];
    $scope.files = [];
    $scope.file = "";

    $scope.modelAdd = {
        UnitId: 2,
        Description: "Add testing",
        PathName: ""
    };

    $scope.modelEdit = {
        UnitId: 2,
        Description: "Edit testing",
        PathName: ""
    };

    $scope.$on("seletedFile", function (event, args) {
        $scope.$apply(function () {
            //add the file object to the scope's files collection  
            //$scope.files.push(args.file);
            $scope.file = args.file;
        });
    });

    $scope.addUnitPhoto = function () {
        var photo = unitphotoService.addUnitPhoto($scope.modelAdd, $scope.file);
        photo.then(function (response) {
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    };

  
    $scope.getUnitPhoto = function () {
        var allResults = unitphotoService.getAllUnitPhoto();
        allResults.then(function (response) {
            $scope.unitPhotos = response.data;
        }, function (error) {
            $scope.message = error.statusText;
            console.log(error);
        })

    } // close function   

    $scope.getUnitPhotoById = function () {
        var resultById = unitphotoService.getByIdUnitPhoto(1);
        resultById.then(function (response) {
            console.log(response.data);
            $scope.imageUrl = response.data.PathName;
            $scope.description = response.data.Description;
            $scope.unitId = response.data.UnitId;
        }, function (error) {
            $scope.message = response.statusText;
        })

    } // close function

    $scope.editUnitPhoto = function () {  
        var photo = unitphotoService.editUnitPhoto($scope.modelEdit, $scope.file);
        photo.then(function (response) {
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    } // close function

    $scope.delete = function (id) {
        var deleteOne = unitphotoService.deleteUnitPhoto(4);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            console.log(response);
        }, function (error) {
            $scope.errorMessage = "Could not delete";
        });
    }


}