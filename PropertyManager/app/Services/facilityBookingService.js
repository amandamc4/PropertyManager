
angular.module("common.services").factory("facilityBookingService", ["$http", "$q", "appSettings", facilityBookingService]);
function facilityBookingService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addFacilityBooking = function (facilityBooking) {
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/FacilityBookings",
            method: "POST",
            data: facilityBooking,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllFacilityBooking = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/FacilityBookings",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdFacilityBooking = function (facilityBookingId) {
        var response = $http({
            url: appSettings.serverPath + "/api/FacilityBookings/" + facilityBookingId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editFacilityBooking = function (facilityBooking, facilityBookingId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/FacilityBookings/" + facilityBookingId,
            method: "PUT",
            data: facilityBooking,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteFacilityBooking = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/FacilityBookings/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addFacilityBooking: this.addFacilityBooking,
        getAllFacilityBooking: this.getAllFacilityBooking,
        getByIdFacilityBooking: this.getByIdFacilityBooking,
        editFacilityBooking: this.editFacilityBooking,
        deleteFacilityBooking: this.deleteFacilityBooking
    }
    
}