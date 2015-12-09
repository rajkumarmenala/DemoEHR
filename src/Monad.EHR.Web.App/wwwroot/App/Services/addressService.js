(function() {
    'use strict';
    angular.module('addressServiceModule', []).factory('addressService', addressService);
    addressService.$inject = ['$http'];
    function addressService($http) {
        var service = {
            getAddresss: getAddresss,
            getAddress: getAddress,
            addAddress: addAddress,
            editAddress: editAddress,
            deleteAddress: deleteAddress,
            getAddressForPatient: getAddressForPatient
        };
        return service;
        function getAddresss(successFunction, errorFunction) {
            $http.get('/api/address/GetAllAddresss').then(successFunction, errorFunction);
        }
        function getAddress(addressId, successFunction, errorFunction) {
            $http.get('/api/address/GetAddress?addressId=' + addressId).then(successFunction, errorFunction);
        }
        function addAddress(address, successFunction, errorFunction) {
            $http.post('/api/address/AddAddress', address).then(successFunction, errorFunction);
        }
        function editAddress(address, successFunction, errorFunction) {
            $http.post('/api/address/EditAddress', address).then(successFunction, errorFunction);
        }
        function deleteAddress(address, successFunction, errorFunction) {
            $http.post('/api/address/DeleteAddress', address).then(successFunction, errorFunction);
        }
        function getAddressForPatient(patientId, successFunction, errorFunction) {
            $http.get('/api/address/GetAddressForPatient?patientId=' + patientId).then(successFunction, errorFunction);
        }
    }
})();
