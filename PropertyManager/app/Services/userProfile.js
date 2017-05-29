
angular.module("common.services").factory("userProfile", userProfile);
function userProfile() {
    var setProfile = function (username, token, role, firstName) {
        sessionStorage.setItem('userName', username);
        sessionStorage.setItem('userRole', role);
        sessionStorage.setItem('accessToken', token);
        sessionStorage.setItem('firstName', firstName);
    };

    var getProfile = function () {
        var profile = {
            isLoggedIn: sessionStorage.getItem('accessToken') != null,
            username: sessionStorage.getItem('userName'),
            userRole: sessionStorage.getItem('userRole'),
            token: sessionStorage.getItem('accessToken'),
            firstName: sessionStorage.getItem('firstName')
        };
        return profile;
    }
    return {
        setProfile: setProfile,
        getProfile: getProfile,
    }
}