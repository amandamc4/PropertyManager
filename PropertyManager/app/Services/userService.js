
angular.module("common.services").factory("userService", ["$http", "$q", "appSettings", userService]);
function userService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.getAllUser = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/account/userGetAll",
            method: "GET",
        });
        return response;
    };

    this.getByEmailUser = function (tenantEmail) {
        var response = $http({
            url: appSettings.serverPath + "/api/account/userGetByEmail/" + tenantEmail + "/find",
            method: "GET",
        });
        return response;
    };

    this.deleteUser = function (email) {
        var response = $http({
            url: appSettings.serverPath + "/api/account/userDelete/" + email + "/delete",
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        getAllUser: this.getAllUser,
        getByEmailUser: this.getByEmailUser,
        deleteUser: this.deleteUser
    }


}