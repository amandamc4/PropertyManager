
angular.module("propertyManagerApp").controller("employeeController", ["$scope", "$filter", '$location', "$routeParams", "employeeService", "userProfile", employeeController]);

function employeeController($scope, $filter, $location, $routeParams, employeeService, userProfile) {

    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;

    if ($routeParams.employee_id) {
        $scope.editId = $routeParams.employee_id;
        $scope.isEdit = true;
        getEmployeeById($scope.editId);
    }
    else {
        getEmployee();
    }

    $scope.modelAdd = {
        employeeId: "",
        LastName: "",
        FirstName: "",
        Title: "",
        Phone: "",
        Email: "",
        BirthDate: "",
        HireDate: "",
        Address: "",
        City: "",
        State: "",
        PostalCode: ""
    };

    $scope.modelEdit = {
        employeeId: "",
        LastName: "",
        FirstName: "",
        Title: "",
        Phone: "",
        Email: "",
        BirthDate: "",
        HireDate: "",
        Address: "",
        City: "",
        State: "",
        PostalCode: ""
    };


    $scope.employees = [];
    $scope.message = "";
    $scope.sortType = 'FirstName';
    $scope.sortReverse = false;
    $scope.searchEmployee = "";

    var today = new Date();
    today.setHours(0, 0, 0, 0);
    $scope.today = today;

    // ADD SECTION 

    $scope.addOneClick = function () {
        $location.path('/addemployee');
    }

    $scope.addEmployee = function () {
        $scope.message = "";
        var add = true;
        
        if ($scope.modelAdd.BirthDate >= $scope.today) {
            $scope.message = "Enter a valid birth date - Must be before current date";
            add = false;
        }

        if (add == true) {
            var BirthDateFiltered = null;
            var HireDateFiltered = null;

            if ($scope.modelAdd.BirthDate != "") {
                BirthDateFiltered = $filter('date')($scope.modelAdd.BirthDate, "yyyy-MM-dd");
            }
            if ($scope.modelAdd.HireDate != "") {
                HireDateFiltered = $filter('date')($scope.modelAdd.HireDate, "yyyy-MM-dd");
            }

            var employee = {
                LastName: $scope.modelAdd.LastName,
                FirstName: $scope.modelAdd.FirstName,
                Title: $scope.modelAdd.Title,
                Address: $scope.modelAdd.Address,
                City: $scope.modelAdd.City,
                State: $scope.modelAdd.State,
                PostalCode: $scope.modelAdd.PostalCode,
                Phone: $scope.modelAdd.Phone,
                Email: $scope.modelAdd.Email,
                BirthDate: BirthDateFiltered,
                HireDate: HireDateFiltered
            };

            var addResults = employeeService.addEmployee(employee);
            addResults.then(function (response) {
                $scope.modelAdd.employeeId = response.data.Id;
                $scope.showConfirmation = true;
                $scope.message = "Employee Added"
            }, function (error) {
                $scope.message = error.statusText + " " + error.status;
            });
        }
    } // close function

    $scope.addAnother = function () {
        $scope.modelAdd = {
            employeeId: "",
            LastName: "",
            FirstName: "",
            Title: "",
            Phone: "",
            Email: "",
            BirthDate: "",
            HireDate: "",
            Address: "",
            City: "",
            State: "",
            PostalCode: ""
        };
        $scope.message = "";
        $scope.form.$setPristine();
        $scope.showConfirmation = false;
    }

    //******************************************************************************************//

    //GET ALL

    function getEmployee() {
        var allEmployees = employeeService.getAllEmployee();
        allEmployees.then(function (response) {
            $scope.employees = response.data;
        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    function getEmployeeById(id) {
        var employeeById = employeeService.getByIdEmployee(id);
        employeeById.then(function (response) {

            $scope.modelEdit.LastName = response.data.LastName;
            $scope.modelEdit.FirstName = response.data.FirstName;
            $scope.modelEdit.Title = response.data.Title;
            $scope.modelEdit.Address = response.data.Address;
            $scope.modelEdit.City = response.data.City;
            $scope.modelEdit.State = response.data.State;
            $scope.modelEdit.PostalCode = response.data.PostalCode;
            $scope.modelEdit.Phone = response.data.Phone;
            $scope.modelEdit.Email = response.data.Email;

            if (response.data.BirthDate != "") {
                $scope.modelEdit.BirthDate = new Date(response.data.BirthDate.replace('T', ' ').replace('-', '/'));
            }
            if (response.data.HireDate != "") {
                $scope.modelEdit.HireDate = new Date(response.data.HireDate.replace('T', ' ').replace('-', '/'));
            }
        }, function (error) {
            $scope.message = error.statusText;
        })

    } // close function

    // *********** EDIT SECTION ******************************************

    $scope.editClick = function (id) {
        $location.path('/addemployee/' + id);
    }
  
    $scope.editEmployee = function () {

        $scope.message = "";
        var add = true;

        if ($scope.modelEdit.BirthDate >= $scope.today) {
            $scope.message = "Enter a valid birth date - Must be before current date";
            add = false;
        }

        if (add == true) {

            var BirthDateFiltered = null;
            var HireDateFiltered = null;

            if ($scope.modelEdit.BirthDate != "") {
                BirthDateFiltered = $filter('date')($scope.modelEdit.BirthDate, "yyyy-MM-dd");
            }
            if ($scope.modelEdit.HireDate != "") {
                HireDateFiltered = $filter('date')($scope.modelEdit.HireDate, "yyyy-MM-dd");
            }

            var employee = {
                Id: $scope.editId,
                LastName: $scope.modelEdit.LastName,
                FirstName: $scope.modelEdit.FirstName,
                Title: $scope.modelEdit.Title,
                Address: $scope.modelEdit.Address,
                City: $scope.modelEdit.City,
                State: $scope.modelEdit.State,
                PostalCode: $scope.modelEdit.PostalCode,
                Phone: $scope.modelEdit.Phone,
                Email: $scope.modelEdit.Email,
                BirthDate: BirthDateFiltered,
                HireDate: HireDateFiltered
            };

            var editResults = employeeService.editEmployee(employee, $scope.editId);
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
        $scope.errorMessage = "";
        var deleteOne = employeeService.deleteEmployee(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getEmployee();
        }, function (error) {
             $scope.message = "Not possible";
            
        });
    }

    $scope.cancelAdd = function () {
        $location.path('/employee');
    }

    $scope.goBack = function () {
        $location.path('/employee');
    }

    // DATE PICKER
    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate', 'MM/dd/yyyy'];
    $scope.format = $scope.formats[4];
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.popup2 = {
        opened: false
    };
    
    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };
}