(function() {
    'use strict';
    angular.module('mainModule').factory('bPService', bPService);
    bPService.$inject = ['$http'];
    function bPService($http) {
        var service = {
            getBPs: getBPs,
            getBP: getBP,
            addBP: addBP,
            editBP: editBP,
            deleteBP: deleteBP,
            getBPForPatient: getBPForPatient
        };
        return service;
        function getBPs(successFunction, errorFunction) {
            $http.get('/api/bP/GetAllBPs').then(successFunction, errorFunction);
        }
        function getBP(bPId, successFunction, errorFunction) {
            $http.get('/api/bP/GetBP?bPId=' + bPId).then(successFunction, errorFunction);
        }
        function addBP(bP, successFunction, errorFunction) {
            $http.post('/api/bP/AddBP', bP).then(successFunction, errorFunction);
        }
        function editBP(bP, successFunction, errorFunction) {
            $http.post('/api/bP/EditBP', bP).then(successFunction, errorFunction);
        }
        function deleteBP(bP, successFunction, errorFunction) {
            $http.post('/api/bP/DeleteBP', bP).then(successFunction, errorFunction);
        }
        function getBPForPatient(patientId, successFunction, errorFunction) {
            $http.get('/api/bP/GetBPForPatient?patientId=' + patientId).then(successFunction, errorFunction);
        }
    }
})();
