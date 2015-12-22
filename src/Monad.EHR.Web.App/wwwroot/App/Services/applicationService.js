(function() {
    'use strict';
    angular.module('applicationServiceModule', []).factory('applicationService', applicationService);
    applicationService.$inject = ['$http'];
    function applicationService($http) {
        var isAuthenticated = false;
        var service = {
            getApplicationTitle: getApplicationTitle,
            initializeApplication: initializeApplication,
            logout: logout,
            getAuthenticated: getAuthenticated,
            setAuthenticated: setAuthenticated,
            getUserClaims: getUserClaims
        };
        return service;
        function getApplicationTitle() {
            return "EHR";
        }
        function initializeApplication(successFunction, errorFunction) {
            successFunction();
        };
        function getAuthenticated() {
            return isAuthenticated;
        };
        function setAuthenticated(value) {
            isAuthenticated = value
        };
        function logout(successFunction, errorFunction) {
            $http.post('/api/account/LogOff').then(successFunction, errorFunction);
        };
        function getUserClaims(successFunction, errorFunction) {
            $http.get('/api/account/GetClaims').then(successFunction, errorFunction);
        };
    }
})();
