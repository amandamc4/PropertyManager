
angular.module("common.services").factory("facilityService", ["$http", "$q", "appSettings", facilityService]);
function facilityService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addFacility = function (facility) {
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/facilities",
            method: "POST",
            data: facility,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllFacility = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/facilities",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdFacility = function (facilityId) {
        var response = $http({
            url: appSettings.serverPath + "/api/facilities/" + facilityId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editFacility = function (facility, facilityId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/facilities/" + facilityId,
            method: "PUT",
            data: facility,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteFacility = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/facilities/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addFacility: this.addFacility,
        getAllFacility: this.getAllFacility,
        getByIdFacility: this.getByIdFacility,
        editFacility: this.editFacility,
        deleteFacility: this.deleteFacility
    }


}