(function() {
    'use strict';
    angular.module('weightServiceModule', []).factory('weightService', weightService);
    weightService.$inject = ['$http'];
    function weightService($http) {
        var service = {
            getWeights: getWeights,
            getWeight: getWeight,
            addWeight: addWeight,
            editWeight: editWeight,
            deleteWeight: deleteWeight,
            getWeightForPatient: getWeightForPatient
        };
        return service;
        function getWeights(successFunction, errorFunction) {
            $http.get('/api/weight/GetAllWeights').then(successFunction, errorFunction);
        }
        function getWeight(weightId, successFunction, errorFunction) {
            $http.get('/api/weight/GetWeight?weightId=' + weightId).then(successFunction, errorFunction);
        }
        function addWeight(weight, successFunction, errorFunction) {
            $http.post('/api/weight/AddWeight', weight).then(successFunction, errorFunction);
        }
        function editWeight(weight, successFunction, errorFunction) {
            $http.post('/api/weight/EditWeight', weight).then(successFunction, errorFunction);
        }
        function deleteWeight(weight, successFunction, errorFunction) {
            $http.post('/api/weight/DeleteWeight', weight).then(successFunction, errorFunction);
        }
        function getWeightForPatient(patientId, successFunction, errorFunction) {
            $http.get('/api/weight/GetWeightForPatient?patientId=' + patientId).then(successFunction, errorFunction);
        }
    }
})();
