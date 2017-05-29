
angular.module("common.services").factory("serviceRequestService", ["$http", "$q", "appSettings", serviceRequestService]);
function serviceRequestService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addServiceRequest = function (serviceRequest) {
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/ServiceRequests",
            method: "POST",
            data: serviceRequest,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllServiceRequest = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/ServiceRequests",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdServiceRequest = function (serviceRequestId) {
        var response = $http({
            url: appSettings.serverPath + "/api/ServiceRequests/" + serviceRequestId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editServiceRequest = function (serviceRequest, serviceRequestId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/ServiceRequests/" + serviceRequestId,
            method: "PUT",
            data: serviceRequest,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteServiceRequest = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/ServiceRequests/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addServiceRequest: this.addServiceRequest,
        getAllServiceRequest: this.getAllServiceRequest,
        getByIdServiceRequest: this.getByIdServiceRequest,
        editServiceRequest: this.editServiceRequest,
        deleteServiceRequest: this.deleteServiceRequest
    }


}