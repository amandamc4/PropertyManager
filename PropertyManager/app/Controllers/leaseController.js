
angular.module("propertyManagerApp").controller("leaseController", ["$scope", "$filter", '$location', "$routeParams", "leaseService", "tenantService", "userProfile", leaseController]);

function leaseController($scope, $filter, $location, $routeParams, leaseService, tenantService, userProfile) {

    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;
    $scope.editMonthlyRent = false;

    var user = userProfile.getProfile();
    $scope.userName = user.username;
    $scope.userRole = user.userRole;

    if ($routeParams.lease_id) {
        $scope.editId = $routeParams.lease_id;
        $scope.isEdit = true;
        getLeaseById($scope.editId);
    }
    else {
        if ($scope.userRole == 'Tenant') {
            getLeaseByEmail();
        }
        else {
            getLease();
        }       
    }


    $scope.modelAdd = {
        leaseId: "",
        startDate: "",
        endDate: "",
        monthlyRent: "",
        securityDeposit: "",
        apartmentId: "",
        tenantId: "",
        tenantName: "",
        tenantEmail: ""
    };

    $scope.modelEdit = {
        leaseId: "",
        startDate: "",
        endDate: "",
        monthlyRent: "",
        securityDeposit: "",
        apartmentId: "",
        tenantId: "",
        tenantName: ""
    };
  
    $scope.message = "";
    $scope.errorMessage = "";
    $scope.leases = [];
    $scope.sortType = "ApartmentNumber";
    $scope.sortReverse = false;
    $scope.searchLease = "";
    $scope.tenantEmail = "";
    $scope.aptNumbers = [];

    // ADD SECTION 

    var today = new Date();
    $scope.today = today;

    $scope.addOneClick = function () {
        $location.path('/addlease');
    }

    $scope.addLease = function () {
        $scope.errorMessage = "";
        var add = false;
        var startDateFiltered = null;
        var endDateFiltered = null;
       
        // VALIDATE DATE
       if ($scope.modelAdd.endDate != "" && $scope.modelAdd.endDate < $scope.modelAdd.startDate) {
           $scope.errorMessage = "Enter a date greater than or equal Start Date";
       }
       else {
            add = true;
       }              
    
    if(add == true){

        if ($scope.modelAdd.startDate != "") {
            startDateFiltered = $filter('date')($scope.modelAdd.startDate, "yyyy-MM-dd");
        }
        if ($scope.modelAdd.expireDate != "") {
            endDateFiltered = $filter('date')($scope.modelAdd.endDate, "yyyy-MM-dd");
        }

        var lease = {
            StartDate: startDateFiltered,
            EndDate: endDateFiltered,
            SecurityDeposit: $scope.modelAdd.securityDeposit,
            MonthlyRent: $scope.modelAdd.monthlyRent,
            ApartmentNumber: $scope.modelAdd.apartmentId,
            TenantId: $scope.modelAdd.tenantId
        };

        // CHECKS IF TENANT EXISTS
        //IF SUCCESSFULL, ADD LEASE FOR THAT TENANT
        //IF NOT, RETURN ERROR MESSAGE
        var tenant = tenantService.getByEmailTenant($scope.modelAdd.tenantEmail);
        tenant.then(function (response) {
            $scope.modelAdd.tenantId = response.data.Id;
            $scope.modelAdd.tenantName = response.data.FirstName + ' ' + response.data.LastName;
            lease.TenantId = response.data.Id;
            return response.data.Id;
        }, function (error) {
            $scope.errorMessage = error.data;
        }).then(function (tenantId) {
            if ($scope.modelAdd.tenantId != "") {
                var addResults = leaseService.addLease(lease);
                addResults.then(function (response) {
                    $scope.modelAdd.leaseId = response.data.Id;
                    $scope.showConfirmation = true;
                    $scope.message = "Lease Added"
                }, function (error) {
                    $scope.errorMessage = error.data;
                    $scope.modelAdd.tenantId = "";
                    $scope.modelAdd.tenantName = "";
                })
            }
            else {
                $scope.errorMessage = "Tenant not found";
            }
        }, function (error) {
        });
    }
                          
    } // close function

    $scope.addAnother = function () {
        $scope.modelAdd = {
            leaseId: "",
            startDate: "",
            endDate: "",
            monthlyRent: "",
            securityDeposit: "",
            apartmentId: "",
            tenantId: "",
            tenantName: "",
            tenantEmail: ""
        };
        $scope.message = "";
        $scope.form.$setPristine();
        $scope.showConfirmation = false;
    }

    // GET ALL
    function getLease() {
        var allLeases = leaseService.getAllLease();
        allLeases.then(function (response) {
            $scope.leases = response.data;
        }, function (error) {
            $scope.message = error.statusText;
        })
    } // close function

    function getLeaseById (id) {
        var leaseById = leaseService.getByIdLease(id);
        leaseById.then(function (response) {      
            $scope.modelEdit.securityDeposit = response.data.SecurityDeposit;
            $scope.modelEdit.monthlyRent = response.data.MonthlyRent;
            $scope.modelEdit.apartmentId = response.data.Apartment.ApartmentNumber;
            $scope.modelEdit.tenantId = response.data.TenantId;
            $scope.modelEdit.tenantName = response.data.Tenant.FirstName + ' ' + response.data.Tenant.LastName;
            if (response.data.StartDate != null) {
                $scope.modelEdit.startDate = new Date(response.data.StartDate.replace('T', ' ').replace('-', '/'));   
            }
            if (response.data.EndDate != null) {
                $scope.modelEdit.endDate = new Date(response.data.EndDate.replace('T', ' ').replace('-', '/'));
                if ($scope.modelEdit.endDate < $scope.today) {
                    $scope.editMonthlyRent = true;
                }
            }

        }, function (error) {
            $scope.message = response.statusText;
        })

    } // close function

    // GET LEASE INFORMATION OF SPECIFIC TENANT
    function getLeaseByEmail() {
        var leases = leaseService.getLeaseByTenantEmail($scope.userName);
        leases.then(function (response) {
            $scope.leaseStartDate = response.data.StartDate;
            $scope.leaseEndDate = response.data.EndDate;
            $scope.leaseSecurityDep = response.data.SecurityDeposit;
            $scope.leaseMonthlyRent = response.data.MonthlyRent;
            $scope.leaseAptNumber = response.data.Apartment.ApartmentNumber;
        }, function (error) {
            $scope.message = error.statusText;
        })
    }

    // *********** EDIT SECTION ******************************************

    $scope.editClick = function (id) {
        $location.path('/addlease/' + id);
    }

    $scope.editLease = function () {
        $scope.errorMessage = "";
        var add = false;
        var startDateFiltered = null;
        var expireDateFiltered = null;

        //VALIDATE DATE
        if ($scope.modelEdit.endDate != "" && $scope.modelEdit.endDate < $scope.modelEdit.startDate) {
            $scope.errorMessage = "Enter a date greater than or equal Start Date";
        }
        else {
            add = true;
        }

        if ($scope.modelEdit.startDate != "") {
            startDateFiltered = $filter('date')($scope.modelEdit.startDate, "yyyy-MM-dd");
        }
        if ($scope.modelEdit.endDate != "") {
            endDateFiltered = $filter('date')($scope.modelEdit.endDate, "yyyy-MM-dd");
        }

        if (add == true) {
            var lease = {
                Id: $scope.editId,
                StartDate: startDateFiltered,
                EndDate: endDateFiltered,
                SecurityDeposit: $scope.modelEdit.securityDeposit,
                MonthlyRent: $scope.modelEdit.monthlyRent,
                ApartmentId: $scope.modelEdit.apartmentId,
                TenantId: $scope.modelEdit.tenantId
            };

            var editResults = leaseService.editLease(lease, lease.Id);
            editResults.then(function (response) {
                $scope.message = "Edit successful";
                $scope.showEditConfirmation = true;
            }, function (error) {
                $scope.message = error.statusText;
            });
        }
    } // close function

    //************** DELETE ************************
    $scope.delete = function (id) {
        var deleteOne = leaseService.deleteLease(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getLease();
        }, function (error) {
            $scope.message = error.statusText;
        });
    }

    $scope.cancelAdd = function () {
        $location.path('/lease');
    }

    $scope.goBack = function () {
        $location.path('/lease');
    }

    // DATE PICKER
    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate', 'MM/dd/yyyy'];
    $scope.format = $scope.formats[4];
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };

    $scope.popup2 = {
        opened: false
    };

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };


}