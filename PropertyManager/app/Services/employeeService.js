
angular.module("common.services").factory("employeeService", ["$http", "$q", "appSettings", employeeService]);
function employeeService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addEmployee = function (employee) {
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/employees",
            method: "POST",
            data: employee,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllEmployee = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/employees",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdEmployee = function (employeeId) {
        var response = $http({
            url: appSettings.serverPath + "/api/employees/" + employeeId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editEmployee = function (employee, employeeId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/employees/" + employeeId,
            method: "PUT",
            data: employee,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteEmployee = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/employees/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addEmployee: this.addEmployee,
        getAllEmployee: this.getAllEmployee,
        getByIdEmployee: this.getByIdEmployee,
        editEmployee: this.editEmployee,
        deleteEmployee: this.deleteEmployee
    }


}