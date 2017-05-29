
angular.module("common.services").factory("apartmentService", ["$http", "$q", "appSettings", apartmentService]);
function apartmentService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addApartment = function (apartment) {
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/apartments",
            method: "POST",
            data: apartment,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllApartment = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/apartments",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdApartment = function (apartmentId) {
        var response = $http({
            url: appSettings.serverPath + "/api/apartments/" + apartmentId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editApartment = function (apartment, apartmentId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/apartments/" + apartmentId,
            method: "PUT",
            data: apartment,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteApartment = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/apartments/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addApartment: this.addApartment,
        getAllApartment: this.getAllApartment,
        getByIdApartment: this.getByIdApartment,
        editApartment: this.editApartment,
        deleteApartment: this.deleteApartment
    }


}