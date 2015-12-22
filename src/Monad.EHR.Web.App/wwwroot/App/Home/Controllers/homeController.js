(function() {
    'use strict';
    var pModule = angular.module('homeModule', ['ngRoute']);
    pModule.config(['$routeProvider', function($routeProvider) {
        $routeProvider.when("/viewPatient", {
            templateUrl: '/app/Patient/Views/PatientList.html',
            controllerUrl: '/app/Patient/Controllers/patientController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewAddress", {
            templateUrl: '/app/Address/Views/AddressList.html',
            controllerUrl: '/app/Address/Controllers/addressController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewMedications", {
            templateUrl: '/app/Medications/Views/MedicationsList.html',
            controllerUrl: '/app/Medications/Controllers/medicationsController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewProblems", {
            templateUrl: '/app/Problems/Views/ProblemsList.html',
            controllerUrl: '/app/Problems/Controllers/problemsController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewBP", {
            templateUrl: '/app/BP/Views/BPList.html',
            controllerUrl: '/app/BP/Controllers/bPController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewPatientHeight", {
            templateUrl: '/app/PatientHeight/Views/PatientHeightList.html',
            controllerUrl: '/app/PatientHeight/Controllers/patientHeightController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewWeight", {
            templateUrl: '/app/Weight/Views/WeightList.html',
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
    }]).controller("homeController", ['$scope', '$injector', '$routeParams', '$cookies', 'userService', function($scope, $injector, $routeParams, $cookies, userService) {
        $scope.initializeController = function() {
            $scope.getUserProfile($cookies.get('currentUserName'));
        }
        $scope.viewForm = function(formName) {
            window.name = window.location.href;
            window.location = "#/" + formName;
        }
        $scope.getUserProfile = function(UserName) {
            userService.showUserProfile(UserName, $scope.getUserProfileCompleted, $scope.getUserProfileError);
        }
        $scope.getUserProfileCompleted = function(response) {
            $scope.User.FirstName = response.data.FirstName;
            $scope.User.LastName = response.data.LastName;
            if (response.data.ProfilePicture != null) {
                $scope.getUploadedImage(response.data.ProfilePicture);
            }
        }
        $scope.getUploadedImage = function(pictureName) {
            userService.getUploadedImage(pictureName, $scope.getUploadedImageCompleted, $scope.getUserProfileError);
        }
        $scope.getUploadedImageCompleted = function(response) {
            $scope.User.imageData = response.data;
        }
    }]);
})();
