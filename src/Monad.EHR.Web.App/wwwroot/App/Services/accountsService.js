(function() {
    'use strict';
    angular.module('accountServiceModule', []).factory('accountsService', accountsService);
    accountsService.$inject = ['$http'];
    function accountsService($http) {
        var service = {
            register: register,
            login: login,
            getUser: getUser,
            updateUser: updateUser,
            getUserClaims: getUserClaims
        };
        return service;
        function register(user, successFunction, errorFunction) {
            $http.post('/api/account/Register', user).then(successFunction, errorFunction);
        }
        function login(user, successFunction, errorFunction) {
            $http.post('/api/account/Login', user).then(successFunction, errorFunction);
        }
        function getUser(successFunction, errorFunction) {}
        function updateUser(user, successFunction, errorFunction) {}
        function authenicateUser(successFunction, errorFunction) { };
        
        function getUserClaims(successFunction, errorFunction) {
            $http.get('/api/account/GetClaims').then(successFunction, errorFunction);
        }
    }
})();
