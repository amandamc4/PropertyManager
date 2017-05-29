
angular.module("propertyManagerApp").controller("headerController", ["$scope", "$location", "userProfile", headerController]);
function headerController($scope, $location, userProfile) {
  
    $scope.Message = "";
    $scope.status;
    $scope.isLoggedIn = "";

    var user = userProfile.getProfile();

    //GET USER ROLE FOR DISPLAYING MENU ITEMS
    $scope.isLoggedIn = user.isLoggedIn;
    $scope.firstName = user.firstName;
    $scope.userRole = user.userRole;

    // LOGOUT - REMOVE SESSION STORAGE
    $scope.logout = function () {
        sessionStorage.removeItem('accessToken');
        sessionStorage.removeItem('userName');
        sessionStorage.removeItem('userRole');
        sessionStorage.removeItem('firstName');
        $scope.isLoggedIn = "";
        $scope.firstName = "";
        $location.path('/index');
    };
}