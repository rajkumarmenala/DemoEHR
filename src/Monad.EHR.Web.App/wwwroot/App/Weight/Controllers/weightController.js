(function() {
    'use strict';
    var pModule = angular.module('weightModule', ['weightServiceModule', 'homeModule', 'popUpModel', 'ngRoute', 'validation', 'validation.rule', 'smart-table']);
    pModule.config(['$routeProvider', '$validationProvider', function($routeProvider, $validationProvider) {
        $validationProvider.showSuccessMessage = false;
        $routeProvider.when("/addWeight", {
            templateUrl: '/app/Weight/Views/WeightAdd.html',
            controllerUrl: '/app/Weight/Controllers/weightController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewWeight/:weightId", {
            templateUrl: '/app/Weight/Views/weightView.html',
            controllerUrl: '/app/Weight/Controllers/weightController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/editWeight/:weightId", {
            templateUrl: '/app/Weight/Views/WeightEdit.html',
            controllerUrl: '/app/Weight/Controllers/weightController.js',
            publicAccess: true,
            sessionAccess: true
        })
    }]).config(['$httpProvider', function($httpProvider) {
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    }]).controller("weightController", ['$scope', '$injector', '$routeParams', 'weightService', '$cookies', '$timeout', function($scope, $injector, $routeParams, weightService, $cookies, $timeout) {
        var $validationProvider = $injector.get('$validation');
        $scope.initializeController = function() {
            var weightId = ($routeParams.weightId || "");
            if (weightId != "") {
                $scope.weightId = weightId;
                $cookies.put('weightId', weightId);
                weightService.getWeight(weightId, $scope.fetchWeightComplete, $scope.fetchWeightError);
            }
            $scope.getWeights();
        }
        // *********************** Get Weight data  ************* 
        $scope.getWeights = function() {
            weightService.getWeights($scope.fetchtWeightsComplete, $scope.fetchtWeightsError);
        }
        // *********************** Get Weight data  ************* 
        // *********************** Fetch Weights  *************
        $scope.fetchWeightComplete = function(response) {
            $timeout(function() {
                var weight = response.data;
                $scope.Date = weight.Date;
                $scope.Wt = weight.Wt;
            },
            10);
        }
        $scope.fetchWeightsError = function(response) {
            //
        }
        // ***********************End Fetch Weight  *************
        // *********************** Fetch Weight  *************
        $scope.fetchtWeightsComplete = function(response) {
            $scope.WeightList = response.data;
            var weight = response.data;
            $scope.Date = weight.Date;
            $scope.Wt = weight.Wt;
        }
        $scope.fetchWeightError = function(response) {
            //
        }
        // ***********************End Fetch Weight  *************
        $scope.addWeight = function(weight) {
            $validationProvider.validate(weight).success($scope.weightAddsuccess).error($scope.weightadderror);
        }
        $scope.weightAddsuccess = function() {
            var weight = $scope.createWeight();
            $scope.$broadcast('show-errors-check-validity');
            if ($scope.AddWeight.$valid) {
                weightService.addWeight(weight, $scope.addNewWeightCompleted, $scope.addError);
            }
        }
        $scope.weightadderror = function() {}
        //*************************** Cancel Functionalities start *******************
        $scope.addNewWeightCancel = function() {
            window.history.back();
            //window.location = "#/" + 'Home';
        }
        $scope.formCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewWeight';
        }
        //*************************** Cancel Functionalities end *******************
        $scope.addNewWeight = function(navigationUrl) {
            window.name = window.location.href;
            window.location = "#/" + navigationUrl;
        }
        $scope.editWeight = function(data) {
            window.name = window.location.href;
            window.location = "#/" + 'editWeight' + "/" + data;
        }
        $scope.viewWeight = function(data) {
            window.name = window.location.href;
            $cookies.put("tabIndex", 0);
            window.location = "#/" + 'viewWeight' + "/" + data;
        }
        $scope.addNewWeightCompleted = function(response) {
            window.history.back();
        }
        $scope.addError = function(response) {
            //
        }
        // *********************** End Add Weight  *************
        // *********************** Edit Weight  *************
        $scope.editWeightSaved = function(weight) {
            $validationProvider.validate(weight).success($scope.success).error($scope.error);
        }
        $scope.success = function() {
            var updateWeight = $scope.createWeight();
            console.log(updateWeight);
            if ($scope.editWeight.$valid) {
                weightService.editWeight(updateWeight, $scope.editWeightCompleted, $scope.editWeightError);
            }
        }
        $scope.error = function() {}
        $scope.editWeightCompleted = function(response) {
            window.history.back();
            //window.location = "#/" + 'viewWeight';
            //window.location=window.name;
        }
        // *********************** End Edit Weight  *************
        $scope.createWeight = function() {
            var weight = new Object();
            weight.Id = $routeParams.weightId;
            weight.Date = $scope.Date;
            weight.Wt = $scope.Wt;
            weight.PatientID = $cookies.get('patientId');
            return weight;
        }
        $scope.weightCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewWeight';
        }
        //********************************* Deleted Item Popup confirmation this is generic code so no need to change anythings.**************************************
        $scope.toggleModal = function(weightId, weightName) {
            $cookies.put('weightId', weightId);
            FormData = {
                'Id': weightId,
                'Name': weightName
            };
            $scope.deleteDescriptionMessage = weightName;
            $scope.ConfirmationMessage = "Are you sure you want to delete.?";
            $scope.showModal = true;
        };
        $scope.popupCancel = function() {
            $scope.showModal = false;
        }
        $scope.deletePopUpConfirmation = function() {
            weightService.deleteWeight(FormData, $scope.deleteWeightCompleted, $scope.fetchFormError);
            $scope.showModal = false;
        }
        $scope.deleteWeight = function(weightId) {
            var deleteData = {
                'Id': weightId
            };
            weightService.deleteWeight(deleteData, $scope.deleteWeightCompleted, $scope.fetchFormError);
        }
        $scope.deleteWeightCompleted = function(data) {
            $timeout(function() {
                $scope.getWeights();
                window.location.reload(true);
                //window.location = "#/" + 'viewWeight';
            },
            500);
        }
    }]);
})();
