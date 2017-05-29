
angular.module("common.services").factory("unitService", ["$http", "$q", "appSettings", unitService]);
function unitService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addUnit = function (unit) {
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/units",
            method: "POST",
            data: unit,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllUnit = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/units",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdUnit = function (unitId) {
        var response = $http({
            url: appSettings.serverPath + "/api/units/" + unitId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editUnit = function (unit, unitId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/units/" + unitId,
            method: "PUT",
            data: unit,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteUnit = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/units/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addUnit: this.addUnit,
        getAllUnit: this.getAllUnit,
        getByIdUnit: this.getByIdUnit,
        editUnit: this.editUnit,
        deleteUnit: this.deleteUnit
    }


}