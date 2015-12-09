(function() {
    'use strict';
    var pModule = angular.module('patientHeightModule', ['patientHeightServiceModule', 'homeModule', 'popUpModel', 'ngRoute', 'validation', 'validation.rule', 'smart-table']);
    pModule.config(['$routeProvider', '$validationProvider', function($routeProvider, $validationProvider) {
        $validationProvider.showSuccessMessage = false;
        $routeProvider.when("/addPatientHeight", {
            templateUrl: '/app/PatientHeight/Views/PatientHeightAdd.html',
            controllerUrl: '/app/PatientHeight/Controllers/patientHeightController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewPatientHeight/:patientHeightId", {
            templateUrl: '/app/PatientHeight/Views/patientHeightView.html',
            controllerUrl: '/app/PatientHeight/Controllers/patientHeightController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/editPatientHeight/:patientHeightId", {
            templateUrl: '/app/PatientHeight/Views/PatientHeightEdit.html',
            controllerUrl: '/app/PatientHeight/Controllers/patientHeightController.js',
            publicAccess: true,
            sessionAccess: true
        })
    }]).config(['$httpProvider', function($httpProvider) {
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    }]).controller("patientHeightController", ['$scope', '$injector', '$routeParams', 'patientHeightService', '$cookies', '$timeout', function($scope, $injector, $routeParams, patientHeightService, $cookies, $timeout) {
        var $validationProvider = $injector.get('$validation');
        $scope.initializeController = function() {
            var patientHeightId = ($routeParams.patientHeightId || "");
            if (patientHeightId != "") {
                $scope.patientHeightId = patientHeightId;
                $cookies.put('patientHeightId', patientHeightId);
                patientHeightService.getPatientHeight(patientHeightId, $scope.fetchPatientHeightComplete, $scope.fetchPatientHeightError);
            }
            $scope.getPatientHeights();
        }
        // *********************** Get PatientHeight data  ************* 
        $scope.getPatientHeights = function() {
            patientHeightService.getPatientHeights($scope.fetchtPatientHeightsComplete, $scope.fetchtPatientHeightsError);
        }
        // *********************** Get PatientHeight data  ************* 
        // *********************** Fetch PatientHeights  *************
        $scope.fetchPatientHeightComplete = function(response) {
            $timeout(function() {
                var patientHeight = response.data;
                $scope.Height = patientHeight.Height;
                $scope.Date = patientHeight.Date;
            },
            10);
        }
        $scope.fetchPatientHeightsError = function(response) {
            //
        }
        // ***********************End Fetch PatientHeight  *************
        // *********************** Fetch PatientHeight  *************
        $scope.fetchtPatientHeightsComplete = function(response) {
            $scope.PatientHeightList = response.data;
            var patientHeight = response.data;
            $scope.Height = patientHeight.Height;
            $scope.Date = patientHeight.Date;
        }
        $scope.fetchPatientHeightError = function(response) {
            //
        }
        // ***********************End Fetch PatientHeight  *************
        $scope.addPatientHeight = function(patientHeight) {
            $validationProvider.validate(patientHeight).success($scope.patientHeightAddsuccess).error($scope.patientHeightadderror);
        }
        $scope.patientHeightAddsuccess = function() {
            var patientHeight = $scope.createPatientHeight();
            $scope.$broadcast('show-errors-check-validity');
            if ($scope.AddPatientHeight.$valid) {
                patientHeightService.addPatientHeight(patientHeight, $scope.addNewPatientHeightCompleted, $scope.addError);
            }
        }
        $scope.patientHeightadderror = function() {}
        //*************************** Cancel Functionalities start *******************
        $scope.addNewPatientHeightCancel = function() {
            window.history.back();
            //window.location = "#/" + 'Home';
        }
        $scope.formCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewPatientHeight';
        }
        //*************************** Cancel Functionalities end *******************
        $scope.addNewPatientHeight = function(navigationUrl) {
            window.name = window.location.href;
            window.location = "#/" + navigationUrl;
        }
        $scope.editPatientHeight = function(data) {
            window.name = window.location.href;
            window.location = "#/" + 'editPatientHeight' + "/" + data;
        }
        $scope.viewPatientHeight = function(data) {
            window.name = window.location.href;
            $cookies.put("tabIndex", 0);
            window.location = "#/" + 'viewPatientHeight' + "/" + data;
        }
        $scope.addNewPatientHeightCompleted = function(response) {
            window.history.back();
        }
        $scope.addError = function(response) {
            //
        }
        // *********************** End Add PatientHeight  *************
        // *********************** Edit PatientHeight  *************
        $scope.editPatientHeightSaved = function(patientHeight) {
            $validationProvider.validate(patientHeight).success($scope.success).error($scope.error);
        }
        $scope.success = function() {
            var updatePatientHeight = $scope.createPatientHeight();
            console.log(updatePatientHeight);
            if ($scope.editPatientHeight.$valid) {
                patientHeightService.editPatientHeight(updatePatientHeight, $scope.editPatientHeightCompleted, $scope.editPatientHeightError);
            }
        }
        $scope.error = function() {}
        $scope.editPatientHeightCompleted = function(response) {
            window.history.back();
            //window.location = "#/" + 'viewPatientHeight';
            //window.location=window.name;
        }
        // *********************** End Edit PatientHeight  *************
        $scope.createPatientHeight = function() {
            var patientHeight = new Object();
            patientHeight.Id = $routeParams.patientHeightId;
            patientHeight.Height = $scope.Height;
            patientHeight.Date = $scope.Date;
            patientHeight.PatientID = $cookies.get('patientId');
            return patientHeight;
        }
        $scope.patientHeightCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewPatientHeight';
        }
        //********************************* Deleted Item Popup confirmation this is generic code so no need to change anythings.**************************************
        $scope.toggleModal = function(patientHeightId, patientHeightName) {
            $cookies.put('patientHeightId', patientHeightId);
            FormData = {
                'Id': patientHeightId,
                'Name': patientHeightName
            };
            $scope.deleteDescriptionMessage = patientHeightName;
            $scope.ConfirmationMessage = "Are you sure you want to delete.?";
            $scope.showModal = true;
        };
        $scope.popupCancel = function() {
            $scope.showModal = false;
        }
        $scope.deletePopUpConfirmation = function() {
            patientHeightService.deletePatientHeight(FormData, $scope.deletePatientHeightCompleted, $scope.fetchFormError);
            $scope.showModal = false;
        }
        $scope.deletePatientHeight = function(patientHeightId) {
            var deleteData = {
                'Id': patientHeightId
            };
            patientHeightService.deletePatientHeight(deleteData, $scope.deletePatientHeightCompleted, $scope.fetchFormError);
        }
        $scope.deletePatientHeightCompleted = function(data) {
            $timeout(function() {
                $scope.getPatientHeights();
                window.location.reload(true);
                //window.location = "#/" + 'viewPatientHeight';
            },
            500);
        }
    }]);
})();
