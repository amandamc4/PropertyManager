
angular.module("common.services").factory("tenantService", ["$http", "$q", "appSettings", tenantService]);
function tenantService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addTenant = function (tenant) {
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/tenants",
            method: "POST",
            data: tenant,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllTenant = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/tenants",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdTenant = function (tenantId) {
        var response = $http({
            url: appSettings.serverPath + "/api/tenants/" + tenantId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByEmailTenant = function (tenantEmail) {
        var response = $http({
            url: appSettings.serverPath + "/api/tenants/email/" + tenantEmail + "/find",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editTenant = function (tenant, tenantId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/tenants/" + tenantId,
            method: "PUT",
            data: tenant,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteTenant = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/tenants/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addTenant: this.addTenant,
        getAllTenant: this.getAllTenant,
        getByIdTenant: this.getByIdTenant,
        getByEmailTenant: this.getByEmailTenant,
        editTenant: this.editTenant,
        deleteTenant: this.deleteTenant
    }


}