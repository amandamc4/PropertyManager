
angular.module("common.services").factory("loginService", ["$http", "appSettings", loginService]);
function loginService($http, appSettings) {

    this.register = function (userInfo) {
        var resp = $http({
            url: appSettings.serverPath + "/api/account/register",
            method: "POST",
            data: userInfo,
        });
        return resp;
    };

    this.login = function (userLogin) {
        var resp = $http({
            url: appSettings.serverPath + "/TOKEN",
            method: "POST",
            data: $.param({ grant_type: 'password', username: userLogin.userName, password: userLogin.password }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        });
        return resp;
    };

    this.getUserInfo = function (email) {
        var resp = $http({
            url: appSettings.serverPath + "/api/account/userGetByEmail/" + email + "/find",
            method: "GET",         
        });
        return resp;
    };

    this.resetPassword = function (userInfo) {
        var resp = $http({
            url: appSettings.serverPath + "/api/account/SetPassword",
            method: "POST",
            data: userInfo,
        });
        return resp;
    };

    this.forgetPassword = function (userInfo) {
        var resp = $http({
            url: appSettings.serverPath + "/api/account/ForgetPassword",
            method: "POST",
            data: userInfo,
        });
        return resp;
    };

    this.sendCode = function (email) {
        var resp = $http({
            url: appSettings.serverPath + "/api/tenants/" + email + "/sendActivation",
            method: "POST"
        });
        return resp;
    };

    return {
        register: this.register,
        login: this.login,
        getUserInfo: this.getUserInfo,
        resetPassword: this.resetPassword,
        forgetPassword: this.forgetPassword,
        sendCode: this.sendCode
    }
}