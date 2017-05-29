
angular.module("common.services").factory("occupantService", ["$http", "$q", "appSettings", occupantService]);
function occupantService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addOccupant = function (occupant) {
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/occupants",
            method: "POST",
            data: occupant,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllOccupant = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/occupants",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdOccupant = function (occupantId) {
        var response = $http({
            url: appSettings.serverPath + "/api/occupants/" + occupantId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editOccupant = function (occupant, occupantId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/occupants/" + occupantId,
            method: "PUT",
            data: occupant,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteOccupant = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/occupants/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addOccupant: this.addOccupant,
        getAllOccupant: this.getAllOccupant,
        getByIdOccupant: this.getByIdOccupant,
        editOccupant: this.editOccupant,
        deleteOccupant: this.deleteOccupant
    }
}