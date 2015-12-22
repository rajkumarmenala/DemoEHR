(function() {
    'use strict';
    angular.module('mainModule').factory('patientHeightService', patientHeightService);
    patientHeightService.$inject = ['$http'];
    function patientHeightService($http) {
        var service = {
            getPatientHeights: getPatientHeights,
            getPatientHeight: getPatientHeight,
            addPatientHeight: addPatientHeight,
            editPatientHeight: editPatientHeight,
            deletePatientHeight: deletePatientHeight,
            getPatientHeightForPatient: getPatientHeightForPatient
        };
        return service;
        function getPatientHeights(successFunction, errorFunction) {
            $http.get('/api/patientHeight/GetAllPatientHeights').then(successFunction, errorFunction);
        }
        function getPatientHeight(patientHeightId, successFunction, errorFunction) {
            $http.get('/api/patientHeight/GetPatientHeight?patientHeightId=' + patientHeightId).then(successFunction, errorFunction);
        }
        function addPatientHeight(patientHeight, successFunction, errorFunction) {
            $http.post('/api/patientHeight/AddPatientHeight', patientHeight).then(successFunction, errorFunction);
        }
        function editPatientHeight(patientHeight, successFunction, errorFunction) {
            $http.post('/api/patientHeight/EditPatientHeight', patientHeight).then(successFunction, errorFunction);
        }
        function deletePatientHeight(patientHeight, successFunction, errorFunction) {
            $http.post('/api/patientHeight/DeletePatientHeight', patientHeight).then(successFunction, errorFunction);
        }
        function getPatientHeightForPatient(patientId, successFunction, errorFunction) {
            $http.get('/api/patientHeight/GetPatientHeightForPatient?patientId=' + patientId).then(successFunction, errorFunction);
        }
    }
})();
