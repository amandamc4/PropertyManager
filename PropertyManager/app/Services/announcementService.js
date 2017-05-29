
angular.module("common.services").factory("announcementService", ["$http", "$q", "appSettings", announcementService]);
function announcementService($http, $q, appSettings) {

    var accessToken = sessionStorage.getItem('accessToken');

    this.addAnnouncement = function (announcement) {
        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/announcements",
            method: "POST",
            data: announcement,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.getAllAnnouncement = function () {
        var response = $http({
            url: appSettings.serverPath + "/api/Announcements",
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.getByIdAnnouncement = function (announcementId) {
        var response = $http({
            url: appSettings.serverPath + "/api/Announcements/" + announcementId,
            method: "GET",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    this.editAnnouncement = function (announcement, announcementId) {

        var def = $q.defer();
        $http({
            url: appSettings.serverPath + "/api/announcements/" + announcementId,
            method: "PUT",
            data: announcement,
            headers: { Authorization: 'Bearer ' + accessToken },
        }).then(function (response) {
            def.resolve(response);
        }, function (err) {
            def.reject(err);
        });
        return def.promise;
    };

    this.deleteAnnouncement = function (id) {
        var response = $http({
            url: appSettings.serverPath + "/api/Announcements/" + id,
            method: "DELETE",
            headers: { Authorization: 'Bearer ' + accessToken },
        });
        return response;
    };

    return {
        addAnnouncement: this.addAnnouncement,
        getAllAnnouncement: this.getAllAnnouncement,
        getByIdAnnouncement: this.getByIdAnnouncement,
        editAnnouncement: this.editAnnouncement,
        deleteAnnouncement: this.deleteAnnouncement
    }


}