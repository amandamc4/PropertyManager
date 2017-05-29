
angular.module("common.services").factory("inventoryService", ["$http", "$q", "appSettings", inventoryService]);
function inventoryService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addInventory = function (inventory) {
        var accessToken = sessionStorage.getItem('accessToken');
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/inventory",
            method: "POST",
            data: inventory,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllInventory = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/inventory",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdInventory = function (inventoryId) {
        var response = $http({
            url: appSettings.serverPath + "/api/inventory/" + inventoryId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editInventory = function (inventory, inventoryId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/inventory/" + inventoryId,
            method: "PUT",
            data: inventory,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteInventory = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/inventory/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addInventory: this.addInventory,
        getAllInventory: this.getAllInventory,
        getByIdInventory: this.getByIdInventory,
        editInventory: this.editInventory,
        deleteInventory: this.deleteInventory
    }


}