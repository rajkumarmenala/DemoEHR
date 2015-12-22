(function() {
    'use strict';
    var mainModule = angular.module('mainModule', ['cacheServiceModule','interceptorServiceModule', 'tokenHandlerServiceModule', 'applicationServiceModule',   'patientModule', 'addressModule', 'medicationsModule', 'problemsModule', 'bPModule', 'patientHeightModule', 'weightModule', 'angular-loading-bar', 'userModule', 'homeModule', 'ngResource', 'ngCookies', 'ngSanitize']);
    mainModule.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider, cacheService, interceptorService, tokenHandlerService) {
        $routeProvider.when("/Home", {
            templateUrl: '/app/Home/Views/Home.html',
            controllerUrl: '/app/Home/Controllers/HomeController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/Login", {
            templateUrl: 'Login.html',
            controllerUrl: '/app/Accounts/Controllers/loginController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/notFound", {
            templateUrl: '/app/Common/Views/NotFound.html',
            publicAccess: true,
            sessionAccess: true
        }).when("/internalServerError", {
            templateUrl: '/app/Common/Views/InternalServerError.html',
            publicAccess: true,
            sessionAccess: true
        }).when("/", {
            redirectTo: '/Home',
            controllerUrl: '/app/Home/Controllers/HomeController.js',
            resolve: {
                arguments: function($location) {
                    //$rootScope.displayContent = true;
                    //if ($cookies.getObject('isAuthenticated') == true) {
                    //    $scope.UserName = $cookies.get('currentUserName');
                    //    $scope.getUserProfile();
                    //}
                    //else {
                    //    // set timeout needed to prevent AngularJS from raising a digest error 
                    //    setTimeout(function () {
                    //        window.location = "/login";
                    //    }, 10);
                    //}
                }
            },
            publicAccess: true,
            sessionAccess: true
        }).otherwise({
            redirectTo: '/notFound',
            templateUrl: '/app/Common/Views/NotFound.html',
            publicAccess: true,
            sessionAccess: true
        });
        $httpProvider.interceptors.push('tokenHandlerService');
        $httpProvider.interceptors.push('interceptorService');
    }]).controller("defaultController", function ($scope, $rootScope, $http, $q, $routeParams, $window, $location, $resource, $cookies, cacheService, applicationService, userService) {
        $scope.initializeController = function() {
            $scope.isAuthenicated = false;
            $scope.UserName = '';
            $scope.User = {};
            applicationService.initializeApplication($scope.initializeApplicationComplete, $scope.initializeApplicationError);
        }
        $scope.initializeApplicationComplete = function(response) {
            $rootScope.displayContent = true;
            if ($cookies.getObject('isAuthenticated') == true) {
                $scope.UserName = $cookies.get('currentUserName');
                applicationService.getUserClaims($scope.claimsFetchCompleted, $scope.claimsFetchError);
                $scope.getUserProfile();
                $scope.title = applicationService.getApplicationTitle();
            }
            else {
                // set timeout needed to prevent AngularJS from raising a digest error 
                setTimeout(function() {
                    window.location = "/login";
                },
                10);
            }
        }

        $scope.claimsFetchCompleted = function (response) {
            cacheService.putValue('accessRights', response.data);
           // console.log(cacheService.getValue('accessRights'));
            // console.log(response.data);
        }

        $scope.claimsFetchError = function (response) {
        }

        $scope.initializeApplicationError = function(response) {}
        $scope.logout = function () {
            console.log(cacheService.getValue('accessRights'));
            alert('Logging out');
            applicationService.logout($scope.logoutCompleted, $scope.logoutError);
        }
        $scope.logoutCompleted = function(response) {
            //if (response.status == 200) {
            //    $cookies.put('currentUserName', null);
            //    $cookies.put('isAuthenticated', false);
            //    window.location = "/login";
            //}
        }
        $scope.logoutError = function(response) {
            $scope.clearValidationErrors();
        }
        $scope.showUserProfile = function() {
            window.location = "/#/userProfile";
        }
        $scope.getUserProfile = function() {
            userService.showUserProfile($scope.UserName, $scope.getUserProfileCompleted, $scope.getUserProfileError);
        }
        $scope.getUserProfileCompleted = function(response) {
            $scope.User = response.data;
        }
        $scope.getUserProfileError = function(response) {
            $scope.clearValidationErrors();
        }
        // Helper Methods
        function TrimDescription(objectData) {
            angular.forEach(objectData, function(index, value) {
                if (index.Description.length >= 28) {
                    var substring = index.Description.substring(0, 20);
                    index.Description = substring + "...";
                }
            });
            return objectData;
        }
        //Project list ending
    });
})();
