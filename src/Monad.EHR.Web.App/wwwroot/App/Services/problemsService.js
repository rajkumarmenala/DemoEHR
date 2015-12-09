(function() {
    'use strict';
    angular.module('problemsServiceModule', []).factory('problemsService', problemsService);
    problemsService.$inject = ['$http'];
    function problemsService($http) {
        var service = {
            getProblemss: getProblemss,
            getProblems: getProblems,
            addProblems: addProblems,
            editProblems: editProblems,
            deleteProblems: deleteProblems,
            getProblemsForPatient: getProblemsForPatient
        };
        return service;
        function getProblemss(successFunction, errorFunction) {
            $http.get('/api/problems/GetAllProblemss').then(successFunction, errorFunction);
        }
        function getProblems(problemsId, successFunction, errorFunction) {
            $http.get('/api/problems/GetProblems?problemsId=' + problemsId).then(successFunction, errorFunction);
        }
        function addProblems(problems, successFunction, errorFunction) {
            $http.post('/api/problems/AddProblems', problems).then(successFunction, errorFunction);
        }
        function editProblems(problems, successFunction, errorFunction) {
            $http.post('/api/problems/EditProblems', problems).then(successFunction, errorFunction);
        }
        function deleteProblems(problems, successFunction, errorFunction) {
            $http.post('/api/problems/DeleteProblems', problems).then(successFunction, errorFunction);
        }
        function getProblemsForPatient(patientId, successFunction, errorFunction) {
            $http.get('/api/problems/GetProblemsForPatient?patientId=' + patientId).then(successFunction, errorFunction);
        }
    }
})();
