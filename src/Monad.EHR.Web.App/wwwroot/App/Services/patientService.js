(function() {
    'use strict';
    angular.module('mainModule').factory('patientService', patientService);
    patientService.$inject = ['$http'];
    function patientService($http) {
        var service = {
            getPatients: getPatients,
            getPatient: getPatient,
            addPatient: addPatient,
            editPatient: editPatient,
            deletePatient: deletePatient
        };
        return service;
        function getPatients(successFunction, errorFunction) {
            $http.get('/api/patient/GetAllPatients').then(successFunction, errorFunction);
        }
        function getPatient(patientId, successFunction, errorFunction) {
            $http.get('/api/patient/GetPatient?patientId=' + patientId).then(successFunction, errorFunction);
        }
        function addPatient(patient, successFunction, errorFunction) {
            $http.post('/api/patient/AddPatient', patient).then(successFunction, errorFunction);
        }
        function editPatient(patient, successFunction, errorFunction) {
            $http.post('/api/patient/EditPatient', patient).then(successFunction, errorFunction);
        }
        function deletePatient(patient, successFunction, errorFunction) {
            $http.post('/api/patient/DeletePatient', patient).then(successFunction, errorFunction);
        }
    }
})();
