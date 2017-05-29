
angular.module("propertyManagerApp").controller("loginController", ["$scope", '$location', "$filter", "loginService", "userProfile", loginController]);

function loginController($scope, $location, $filter, loginService, userProfile) {
    $scope.responseData = "";
    $scope.userName = "";
    $scope.userEmail = "";
    $scope.userPassword = "";
    $scope.userFirstName = "";
    $scope.userLastName = "";
    $scope.accessToken = "";
    $scope.refreshToken = "";
    $scope.isLoggedIn = false;
    $scope.passForget = false;
    $scope.userLastName = "";
    $scope.birthDate = "";
    $scope.confirmPassword = "";
    $scope.userResetPassword = "";
    $scope.message = "";
    $scope.errorMessage = "";
    $scope.forgetMessage = "";


    $scope.registerUser = function () {
        $location.path('/register');
    };

    // LOGIN AND GET USER INFO
    $scope.login = function () {
        $scope.dataLoading = true;
        var userLogin = {
            grant_type: 'password',
            userName: $scope.userEmail,
            password: $scope.userPassword
        };
        $scope.message = "";
        var loginResult = loginService.login(userLogin);
        loginResult.then(function (response) {
            $scope.userName = response.data.userName;
            return response.data;
        }).
        then(function (data){
            var getUserResult = loginService.getUserInfo(data.userName);
            getUserResult.then(function (response) {              
                userProfile.setProfile(response.data.UserName, data.access_token, response.data.Role, response.data.GivenName);
                $scope.isLoggedIn = true;
                if (response.data.Role == "Tenant") {
                    $location.path('/tenanthome');
                }
                else {
                    $location.path('/home');
                }                                  
            }, function (error) {
                $scope.message = response.statusText + " : \r\n";
                if (error.data.error) {
                    $scope.message += error.data.error_description;
                }
            })            
        }, function (response) {     
            $scope.message = response.data.error_description;
        });
    };

    $scope.forgetPassword = function () {
        $scope.passForget = true;
    }

    // PASSWORD RESET
    $scope.resetPasword = function () {
        $scope.message = "";
        var user = userProfile.getProfile();

        var userInfo = {
            NewPassword: $scope.userResetPassword,
            ConfirmPassword: $scope.userResetPassword,
            Email: user.username,
        };
        var resetResult = loginService.resetPassword(userInfo);
        resetResult.then(function (data) {
            $scope.forgetMessage = "Reset Password Successfull";
            $scope.formReset.$setPristine();
            $scope.userResetPassword = "";
            $scope.confirmPassword = "";
        }, function (response) {
            $scope.errorMessage = response.data;
        });
    };

    $scope.cancelReset = function () {
        $scope.passForget = false;
    }

    // PASSWORD FORGET - SEND TEMPORARY PASSWORD BY EMAIL
    $scope.forgetPasword = function () {
        $scope.errorMessage = "";
        $scope.message = "";

        var userInfo = {           
            Email: $scope.userEmail
        };
        var resetResult = loginService.forgetPassword(userInfo);
        resetResult.then(function (data) {
            $scope.message = "Password sent to email";
            $scope.formReset.$setPristine();
            $scope.userEmail = "";
            $scope.passForget = false;
        }, function (response) {
            $scope.errorMessage = response.data;
        });
    };
}