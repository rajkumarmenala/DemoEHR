(function() {
    'use strict';
    var pModule = angular.module('addressModule', [ 'homeModule', 'popUpModel', 'ngRoute', 'validation', 'validation.rule', 'smart-table']);
    pModule.config(['$routeProvider', '$validationProvider', function($routeProvider, $validationProvider) {
        $validationProvider.showSuccessMessage = false;
        $routeProvider.when("/addAddress", {
            templateUrl: '/app/Address/Views/AddressAdd.html',
            controllerUrl: '/app/Address/Controllers/addressController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewAddress/:addressId", {
            templateUrl: '/app/Address/Views/addressView.html',
            controllerUrl: '/app/Address/Controllers/addressController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/editAddress/:addressId", {
            templateUrl: '/app/Address/Views/AddressEdit.html',
            controllerUrl: '/app/Address/Controllers/addressController.js',
            publicAccess: true,
            sessionAccess: true
        })
    }]).config(['$httpProvider', function($httpProvider) {
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    }]).controller("addressController", function ($scope, $injector, $routeParams, cacheService, addressService, $cookies, $timeout) {
        var $validationProvider = $injector.get('$validation');
        $scope.initializeController = function() {
            var addressId = ($routeParams.addressId || "");
            if (addressId != "") {
                $scope.addressId = addressId;
                $cookies.put('addressId', addressId);
                addressService.getAddress(addressId, $scope.fetchAddressComplete, $scope.fetchAddressError);
            }
            $scope.getAddresss();
        }
        // *********************** Get Address data  ************* 
        $scope.getAddresss = function () {
            //console.log(cacheService.getValue('accessRights'));
            //alert('Pulled');
            addressService.getAddresss($scope.fetchtAddresssComplete, $scope.fetchtAddresssError);
        }
        // *********************** Get Address data  ************* 
        // *********************** Fetch Addresss  *************
        $scope.fetchAddressComplete = function(response) {
            $timeout(function() {
                var address = response.data;
                $scope.Line1 = address.Line1;
                $scope.Line2 = address.Line2;
                $scope.City = address.City;
                $scope.State = address.State;
                $scope.Zip = address.Zip;
                $scope.BeginDate = address.BeginDate;
                $scope.EndDate = address.EndDate;
            },
            10);
        }
        $scope.fetchAddresssError = function(response) {
            //
        }
        // ***********************End Fetch Address  *************
        // *********************** Fetch Address  *************
        $scope.fetchtAddresssComplete = function(response) {
            $scope.AddressList = response.data;
            var address = response.data;
            $scope.Line1 = address.Line1;
            $scope.Line2 = address.Line2;
            $scope.City = address.City;
            $scope.State = address.State;
            $scope.Zip = address.Zip;
            $scope.BeginDate = address.BeginDate;
            $scope.EndDate = address.EndDate;
        }
        $scope.fetchAddressError = function(response) {
            //
        }
        // ***********************End Fetch Address  *************
        $scope.addAddress = function(address) {
            $validationProvider.validate(address).success($scope.addressAddsuccess).error($scope.addressadderror);
        }
        $scope.addressAddsuccess = function() {
            var address = $scope.createAddress();
            $scope.$broadcast('show-errors-check-validity');
            if ($scope.AddAddress.$valid) {
                addressService.addAddress(address, $scope.addNewAddressCompleted, $scope.addError);
            }
        }
        $scope.addressadderror = function() {}
        //*************************** Cancel Functionalities start *******************
        $scope.addNewAddressCancel = function() {
            window.history.back();
            //window.location = "#/" + 'Home';
        }
        $scope.formCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewAddress';
        }
        //*************************** Cancel Functionalities end *******************
        $scope.addNewAddress = function(navigationUrl) {
            window.name = window.location.href;
            window.location = "#/" + navigationUrl;
        }
        $scope.editAddress = function(data) {
            window.name = window.location.href;
            window.location = "#/" + 'editAddress' + "/" + data;
        }
        $scope.viewAddress = function(data) {
            window.name = window.location.href;
            $cookies.put("tabIndex", 0);
            window.location = "#/" + 'viewAddress' + "/" + data;
        }
        $scope.addNewAddressCompleted = function(response) {
            window.history.back();
        }
        $scope.addError = function(response) {
            //
        }
        // *********************** End Add Address  *************
        // *********************** Edit Address  *************
        $scope.editAddressSaved = function(address) {
            $validationProvider.validate(address).success($scope.success).error($scope.error);
        }
        $scope.success = function() {
            var updateAddress = $scope.createAddress();
            console.log(updateAddress);
            if ($scope.editAddress.$valid) {
                addressService.editAddress(updateAddress, $scope.editAddressCompleted, $scope.editAddressError);
            }
        }
        $scope.error = function() {}
        $scope.editAddressCompleted = function(response) {
            window.history.back();
            //window.location = "#/" + 'viewAddress';
            //window.location=window.name;
        }
        // *********************** End Edit Address  *************
        $scope.createAddress = function() {
            var address = new Object();
            address.Id = $routeParams.addressId;
            address.Line1 = $scope.Line1;
            address.Line2 = $scope.Line2;
            address.City = $scope.City;
            address.State = $scope.State;
            address.Zip = $scope.Zip;
            address.BeginDate = $scope.BeginDate;
            address.EndDate = $scope.EndDate;
            address.PatientID = $cookies.get('patientId');
            return address;
        }
        $scope.addressCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewAddress';
        }
        //********************************* Deleted Item Popup confirmation this is generic code so no need to change anythings.**************************************
        $scope.toggleModal = function(addressId, addressName) {
            $cookies.put('addressId', addressId);
            FormData = {
                'Id': addressId,
                'Name': addressName
            };
            $scope.deleteDescriptionMessage = addressName;
            $scope.ConfirmationMessage = "Are you sure you want to delete.?";
            $scope.showModal = true;
        };
        $scope.popupCancel = function() {
            $scope.showModal = false;
        }
        $scope.deletePopUpConfirmation = function() {
            addressService.deleteAddress(FormData, $scope.deleteAddressCompleted, $scope.fetchFormError);
            $scope.showModal = false;
        }
        $scope.deleteAddress = function(addressId) {
            var deleteData = {
                'Id': addressId
            };
            addressService.deleteAddress(deleteData, $scope.deleteAddressCompleted, $scope.fetchFormError);
        }
        $scope.deleteAddressCompleted = function(data) {
            $timeout(function() {
                $scope.getAddresss();
                window.location.reload(true);
                //window.location = "#/" + 'viewAddress';
            },
            500);
        }
    });
})();
