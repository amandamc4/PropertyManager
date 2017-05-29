angular.module("propertyManagerApp").controller("announcementController", ["$scope", "$filter", '$location', "$routeParams", "announcementService", "userProfile", announcementController]);

function announcementController($scope, $filter, $location, $routeParams, announcementService, userProfile) {
    $scope.editId = "";
    $scope.isEdit = false;
    $scope.showEditConfirmation = false;

    if ($routeParams.announce_id) {
        $scope.editId = $routeParams.announce_id;
        $scope.isEdit = true;
        getAnnouncementById($scope.editId);
    }
    else {
        getAnnouncement();
    }
    

    $scope.modelAdd = {
        announcementId: "",
        title: "",
        startDate: "",
        expireDate: "",
        description: ""
    };

    $scope.modelEdit= {
        announcementId: "",
        title: "",
        startDate: "",
        expireDate: "",
        description: ""
    };
  
    $scope.startDateDetail = "";
    $scope.expireDateDetail = "";
    $scope.message = "";
    $scope.errorMessage = "";
    $scope.announces = [];
    $scope.sortType = "name";
    $scope.sortReverse = false;
    $scope.searchAnnouncement = "";
    $scope.addOne = false;
    $scope.showConfirmation = false;
   

    // ADD SECTION 

    var today = new Date();
    today.setHours(0,0,0,0);
    $scope.today = today;
    
    $scope.addOneClick = function () {
        $location.path('/addannouncement');
    }

    $scope.addAnnouncement = function () {
        $scope.message = "";
        var add = false;

        if ($scope.modelAdd.startDate < $scope.today) {
            $scope.message = "Enter a date greater or equal than today";
        }
        else {
            if ($scope.modelAdd.expireDate != null) {
                if ($scope.modelAdd.expireDate != "" && $scope.modelAdd.expireDate < $scope.modelAdd.startDate) {
                $scope.message = "Enter a date greater than or equal Start Date";
                }
                else {
                    add = true;
                }
            }           
            else {
                add = true;
            }
        }
        
        if (add == true) {

            var startDateFiltered = null;
            var expireDateFiltered = null;

            if ($scope.modelAdd.startDate != "") {
                startDateFiltered = $filter('date')($scope.modelAdd.startDate, "yyyy-MM-dd");
            }
            if ($scope.modelAdd.expireDate != "") {
                expireDateFiltered = $filter('date')($scope.modelAdd.expireDate, "yyyy-MM-dd");
            }

            var announcement = {
                Title: $scope.modelAdd.title,
                StartDate: startDateFiltered,
                ExpireDate: expireDateFiltered,
                Description: $scope.modelAdd.description
            };

            var addResults = announcementService.addAnnouncement(announcement);
            addResults.then(function (response) {
                $scope.modelAdd.announcementId = response.data.Id;
                $scope.showConfirmation = true;
                $scope.message = "Announcement Added"
            }, function (error) {
                $scope.message = error.statusText + " " + error.status;
            });
        }
    } // close function

    $scope.addAnother = function () {
        $scope.modelAdd = {
            announcementId: "",
            title: "",
            startDate: "",
            expireDate: "",
            description: ""
        };
        $scope.message = "";
        $scope.form.$setPristine();
        $scope.showConfirmation = false;
    }


    //******************************************************************************************//

    //GET ALL
    function getAnnouncement () {
        var allAnnounces = announcementService.getAllAnnouncement();
        allAnnounces.then(function (response) {
            $scope.announces = response.data;
        }, function (error){
            $scope.message = error.statusText;       
        })

    } // close function

    function getAnnouncementById(id) {
        var announceById = announcementService.getByIdAnnouncement(id);
        announceById.then(function (response) {
            $scope.modelEdit.title = response.data.Title;
            $scope.modelEdit.description = response.data.Description;
            if (response.data.StartDate != null) {              
                $scope.modelEdit.startDate = new Date(response.data.StartDate.replace('T', ' ').replace('-', '/'));
                $scope.startDateDetail = $scope.modelEdit.startDate.toString().substring(0, 15);
            }
            if (response.data.ExpireDate != null) {
                $scope.modelEdit.expireDate = new Date(response.data.ExpireDate.replace('T', ' ').replace('-', '/'));
                $scope.expireDateDetail = $scope.modelEdit.expireDate.toString().substring(0, 15);
            }
            
        }, function (error){
            $scope.message = error.statusText;
        })

    } // close function


    // *********** EDIT SECTION ******************************************
    

    $scope.editClick = function (id) {
        $scope.message = "";
        $location.path('/addannouncement/' + id);
    }

    $scope.editAnnouncement = function () {

        var add = false;

        if ($scope.modelEdit.startDate < $scope.today) {
            $scope.message = "Enter a date greater than today";
        }
        else {
            if ($scope.modelEdit.expireDate != null) {
                if ($scope.modelEdit.expireDate != "" && $scope.modelEdit.expireDate < $scope.modelEdit.startDate) {
                    $scope.message = "Enter a date greater than or equal Start Date";
                }
                else {
                    add = true;
                }
            }           
            else {
                add = true;
            }
        }

        if (add == true) {
            var startDateFiltered = null;
            var expireDateFiltered = null;

            if ($scope.modelEdit.startDate != "") {
                startDateFiltered = $filter('date')($scope.modelEdit.startDate, "yyyy-MM-dd");
            }
            if ($scope.modelEdit.expireDate != "") {
                expireDateFiltered = $filter('date')($scope.modelEdit.expireDate, "yyyy-MM-dd");
            }

            var announcement = {
                Id: $scope.editId,
                Title: $scope.modelEdit.title,
                StartDate: startDateFiltered,
                ExpireDate: expireDateFiltered,
                Description: $scope.modelEdit.description
            };

            var editResults = announcementService.editAnnouncement(announcement, announcement.Id);
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
        var deleteOne = announcementService.deleteAnnouncement(id);
        deleteOne.then(function (response) {
            $scope.message = "Delete successfull";
            getAnnouncement();
        }, function (error) {
            $scope.message = error.statusText;
        });
    }

    $scope.cancelAdd = function () {
        $location.path('/announcement');
    }

    $scope.goBack = function () {
        $location.path('/announcement');
    }

    $scope.greaterThan = function (item) {
        return function (item) {
            if (item.ExpireDate != null) {
                var announceDate = new Date(item.ExpireDate.replace('T', ' ').replace('-', '/'));
                return item.ExpireDate >= today.getFullYear();
            }
            else {
                return item;
            }
        }
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