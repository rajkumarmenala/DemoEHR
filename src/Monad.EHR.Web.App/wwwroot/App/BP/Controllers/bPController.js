(function() {
    'use strict';
    var pModule = angular.module('bPModule', ['bPServiceModule', 'homeModule', 'popUpModel', 'ngRoute', 'validation', 'validation.rule', 'smart-table']);
    pModule.config(['$routeProvider', '$validationProvider', function($routeProvider, $validationProvider) {
        $validationProvider.showSuccessMessage = false;
        $routeProvider.when("/addBP", {
            templateUrl: '/app/BP/Views/BPAdd.html',
            controllerUrl: '/app/BP/Controllers/bPController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewBP/:bPId", {
            templateUrl: '/app/BP/Views/bPView.html',
            controllerUrl: '/app/BP/Controllers/bPController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/editBP/:bPId", {
            templateUrl: '/app/BP/Views/BPEdit.html',
            controllerUrl: '/app/BP/Controllers/bPController.js',
            publicAccess: true,
            sessionAccess: true
        })
    }]).config(['$httpProvider', function($httpProvider) {
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    }]).controller("bPController", ['$scope', '$injector', '$routeParams', 'bPService', '$cookies', '$timeout', function($scope, $injector, $routeParams, bPService, $cookies, $timeout) {
        var $validationProvider = $injector.get('$validation');
        $scope.initializeController = function() {
            var bPId = ($routeParams.bPId || "");
            if (bPId != "") {
                $scope.bPId = bPId;
                $cookies.put('bPId', bPId);
                bPService.getBP(bPId, $scope.fetchBPComplete, $scope.fetchBPError);
            }
            $scope.getBPs();
        }
        // *********************** Get BP data  ************* 
        $scope.getBPs = function() {
            bPService.getBPs($scope.fetchtBPsComplete, $scope.fetchtBPsError);
        }
        // *********************** Get BP data  ************* 
        // *********************** Fetch BPs  *************
        $scope.fetchBPComplete = function(response) {
            $timeout(function() {
                var bP = response.data;
                $scope.Systolic = bP.Systolic;
                $scope.Diastolic = bP.Diastolic;
                $scope.Date = bP.Date;
            },
            10);
        }
        $scope.fetchBPsError = function(response) {
            //
        }
        // ***********************End Fetch BP  *************
        // *********************** Fetch BP  *************
        $scope.fetchtBPsComplete = function(response) {
            $scope.BPList = response.data;
            var bP = response.data;
            $scope.Systolic = bP.Systolic;
            $scope.Diastolic = bP.Diastolic;
            $scope.Date = bP.Date;
        }
        $scope.fetchBPError = function(response) {
            //
        }
        // ***********************End Fetch BP  *************
        $scope.addBP = function(bP) {
            $validationProvider.validate(bP).success($scope.bPAddsuccess).error($scope.bPadderror);
        }
        $scope.bPAddsuccess = function() {
            var bP = $scope.createBP();
            $scope.$broadcast('show-errors-check-validity');
            if ($scope.AddBP.$valid) {
                bPService.addBP(bP, $scope.addNewBPCompleted, $scope.addError);
            }
        }
        $scope.bPadderror = function() {}
        //*************************** Cancel Functionalities start *******************
        $scope.addNewBPCancel = function() {
            window.history.back();
            //window.location = "#/" + 'Home';
        }
        $scope.formCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewBP';
        }
        //*************************** Cancel Functionalities end *******************
        $scope.addNewBP = function(navigationUrl) {
            window.name = window.location.href;
            window.location = "#/" + navigationUrl;
        }
        $scope.editBP = function(data) {
            window.name = window.location.href;
            window.location = "#/" + 'editBP' + "/" + data;
        }
        $scope.viewBP = function(data) {
            window.name = window.location.href;
            $cookies.put("tabIndex", 0);
            window.location = "#/" + 'viewBP' + "/" + data;
        }
        $scope.addNewBPCompleted = function(response) {
            window.history.back();
        }
        $scope.addError = function(response) {
            //
        }
        // *********************** End Add BP  *************
        // *********************** Edit BP  *************
        $scope.editBPSaved = function(bP) {
            $validationProvider.validate(bP).success($scope.success).error($scope.error);
        }
        $scope.success = function() {
            var updateBP = $scope.createBP();
            console.log(updateBP);
            if ($scope.editBP.$valid) {
                bPService.editBP(updateBP, $scope.editBPCompleted, $scope.editBPError);
            }
        }
        $scope.error = function() {}
        $scope.editBPCompleted = function(response) {
            window.history.back();
            //window.location = "#/" + 'viewBP';
            //window.location=window.name;
        }
        // *********************** End Edit BP  *************
        $scope.createBP = function() {
            var bP = new Object();
            bP.Id = $routeParams.bPId;
            bP.Systolic = $scope.Systolic;
            bP.Diastolic = $scope.Diastolic;
            bP.Date = $scope.Date;
            bP.PatientID = $cookies.get('patientId');
            return bP;
        }
        $scope.bPCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewBP';
        }
        //********************************* Deleted Item Popup confirmation this is generic code so no need to change anythings.**************************************
        $scope.toggleModal = function(bPId, bPName) {
            $cookies.put('bPId', bPId);
            FormData = {
                'Id': bPId,
                'Name': bPName
            };
            $scope.deleteDescriptionMessage = bPName;
            $scope.ConfirmationMessage = "Are you sure you want to delete.?";
            $scope.showModal = true;
        };
        $scope.popupCancel = function() {
            $scope.showModal = false;
        }
        $scope.deletePopUpConfirmation = function() {
            bPService.deleteBP(FormData, $scope.deleteBPCompleted, $scope.fetchFormError);
            $scope.showModal = false;
        }
        $scope.deleteBP = function(bPId) {
            var deleteData = {
                'Id': bPId
            };
            bPService.deleteBP(deleteData, $scope.deleteBPCompleted, $scope.fetchFormError);
        }
        $scope.deleteBPCompleted = function(data) {
            $timeout(function() {
                $scope.getBPs();
                window.location.reload(true);
                //window.location = "#/" + 'viewBP';
            },
            500);
        }
    }]);
})();
