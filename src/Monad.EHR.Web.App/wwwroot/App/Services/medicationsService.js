(function() {
    'use strict';
    angular.module('medicationsServiceModule', []).factory('medicationsService', medicationsService);
    medicationsService.$inject = ['$http'];
    function medicationsService($http) {
        var service = {
            getMedicationss: getMedicationss,
            getMedications: getMedications,
            addMedications: addMedications,
            editMedications: editMedications,
            deleteMedications: deleteMedications,
            getMedicationsForPatient: getMedicationsForPatient
        };
        return service;
        function getMedicationss(successFunction, errorFunction) {
            $http.get('/api/medications/GetAllMedicationss').then(successFunction, errorFunction);
        }
        function getMedications(medicationsId, successFunction, errorFunction) {
            $http.get('/api/medications/GetMedications?medicationsId=' + medicationsId).then(successFunction, errorFunction);
        }
        function addMedications(medications, successFunction, errorFunction) {
            $http.post('/api/medications/AddMedications', medications).then(successFunction, errorFunction);
        }
        function editMedications(medications, successFunction, errorFunction) {
            $http.post('/api/medications/EditMedications', medications).then(successFunction, errorFunction);
        }
        function deleteMedications(medications, successFunction, errorFunction) {
            $http.post('/api/medications/DeleteMedications', medications).then(successFunction, errorFunction);
        }
        function getMedicationsForPatient(patientId, successFunction, errorFunction) {
            $http.get('/api/medications/GetMedicationsForPatient?patientId=' + patientId).then(successFunction, errorFunction);
        }
    }
})();
