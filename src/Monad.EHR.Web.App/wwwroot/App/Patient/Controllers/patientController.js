(function() {
    'use strict';
    var pModule = angular.module('patientModule', [ 'homeModule', 'popUpModel', 'ngRoute', 'validation', 'validation.rule', 'smart-table']);
    pModule.config(['$routeProvider', '$validationProvider', function($routeProvider, $validationProvider) {
        $validationProvider.showSuccessMessage = false;
        $routeProvider.when("/addPatient", {
            templateUrl: '/app/Patient/Views/PatientAdd.html',
            controllerUrl: '/app/Patient/Controllers/patientController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewPatient/:patientId", {
            templateUrl: '/app/Patient/Views/patientSubformsTab.html',
            controllerUrl: '/app/Patient/Controllers/patientController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/editPatient/:patientId", {
            templateUrl: '/app/Patient/Views/PatientEdit.html',
            controllerUrl: '/app/Patient/Controllers/patientController.js',
            publicAccess: true,
            sessionAccess: true
        })
    }]).config(['$httpProvider', function($httpProvider) {
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    }]).controller("patientController", function ($scope, $injector, $routeParams, cacheService, patientService, addressService, medicationsService, problemsService, bPService, patientHeightService, weightService, $cookies, $timeout) {
        var $validationProvider = $injector.get('$validation');
        $scope.initializeController = function() {
            var patientId = ($routeParams.patientId || "");
            if (patientId != "") {
                $scope.patientId = patientId;
                $cookies.put('patientId', patientId);
                patientService.getPatient(patientId, $scope.fetchPatientComplete, $scope.fetchPatientError);
            }
            $scope.getPatients();
        }
        // *********************** Get Patient data  ************* 
        $scope.getPatients = function () {
            //console.log(cacheService.getValue('accessRights'));
            //alert('Pulled');
            patientService.getPatients($scope.fetchtPatientsComplete, $scope.fetchtPatientsError);
        }
        // *********************** Get Patient data  ************* 
        // *********************** Fetch Patients  *************
        $scope.fetchPatientComplete = function(response) {
            $timeout(function() {
                var patient = response.data;
                $scope.FirstName = patient.FirstName;
                $scope.LastName = patient.LastName;
                $scope.DOB = patient.DOB;
                $scope.SSN = patient.SSN;
                $scope.Email = patient.Email;
                $scope.Phone = patient.Phone;
            },
            10);
        }
        $scope.fetchPatientsError = function(response) {
            //
        }
        // ***********************End Fetch Patient  *************
        // *********************** Fetch Patient  *************
        $scope.fetchtPatientsComplete = function(response) {
            $scope.PatientList = response.data;
            var patient = response.data;
            $scope.FirstName = patient.FirstName;
            $scope.LastName = patient.LastName;
            $scope.DOB = patient.DOB;
            $scope.SSN = patient.SSN;
            $scope.Email = patient.Email;
            $scope.Phone = patient.Phone;
        }
        $scope.fetchPatientError = function(response) {
            //
        }
        // ***********************End Fetch Patient  *************
        $scope.addPatient = function(patient) {
            $validationProvider.validate(patient).success($scope.patientAddsuccess).error($scope.patientadderror);
        }
        $scope.patientAddsuccess = function() {
            var patient = $scope.createPatient();
            $scope.$broadcast('show-errors-check-validity');
            if ($scope.AddPatient.$valid) {
                patientService.addPatient(patient, $scope.addNewPatientCompleted, $scope.addError);
            }
        }
        $scope.patientadderror = function() {}
        //*************************** Cancel Functionalities start *******************
        $scope.addNewPatientCancel = function() {
            window.history.back();
            //window.location = "#/" + 'Home';
        }
        $scope.formCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewPatient';
        }
        //*************************** Cancel Functionalities end *******************
        $scope.addNewPatient = function(navigationUrl) {
            window.name = window.location.href;
            window.location = "#/" + navigationUrl;
        }
        $scope.editPatient = function(data) {
            window.name = window.location.href;
            window.location = "#/" + 'editPatient' + "/" + data;
        }
        $scope.viewPatient = function(data) {
            window.name = window.location.href;
            $cookies.put("tabIndex", 0);
            window.location = "#/" + 'viewPatient' + "/" + data;
        }
        $scope.addNewPatientCompleted = function(response) {
            window.history.back();
        }
        $scope.addError = function(response) {
            //
        }
        // *********************** End Add Patient  *************
        // *********************** Edit Patient  *************
        $scope.editPatientSaved = function(patient) {
            $validationProvider.validate(patient).success($scope.success).error($scope.error);
        }
        $scope.success = function() {
            var updatePatient = $scope.createPatient();
            console.log(updatePatient);
            if ($scope.editPatient.$valid) {
                patientService.editPatient(updatePatient, $scope.editPatientCompleted, $scope.editPatientError);
            }
        }
        $scope.error = function() {}
        $scope.editPatientCompleted = function(response) {
            window.history.back();
            //window.location = "#/" + 'viewPatient';
            //window.location=window.name;
        }
        // *********************** End Edit Patient  *************
        $scope.createPatient = function() {
            var patient = new Object();
            patient.Id = $routeParams.patientId;
            patient.FirstName = $scope.FirstName;
            patient.LastName = $scope.LastName;
            patient.DOB = $scope.DOB;
            patient.SSN = $scope.SSN;
            patient.Email = $scope.Email;
            patient.Phone = $scope.Phone;
            return patient;
        }
        $scope.patientCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewPatient';
        }
        //********************************* Deleted Item Popup confirmation this is generic code so no need to change anythings.**************************************
        $scope.toggleModal = function(patientId, patientName) {
            $cookies.put('patientId', patientId);
            FormData = {
                'Id': patientId,
                'Name': patientName
            };
            $scope.deleteDescriptionMessage = patientName;
            $scope.ConfirmationMessage = "Are you sure you want to delete.?";
            $scope.showModal = true;
        };
        $scope.popupCancel = function() {
            $scope.showModal = false;
        }
        $scope.deletePopUpConfirmation = function() {
            patientService.deletePatient(FormData, $scope.deletePatientCompleted, $scope.fetchFormError);
            $scope.showModal = false;
        }
        $scope.deletePatient = function(patientId) {
            var deleteData = {
                'Id': patientId
            };
            patientService.deletePatient(deleteData, $scope.deletePatientCompleted, $scope.fetchFormError);
        }
        $scope.deletePatientCompleted = function(data) {
            $timeout(function() {
                $scope.getPatients();
                window.location.reload(true);
                //window.location = "#/" + 'viewPatient';
            },
            500);
        }
        //************************ Address Functionalities****************************************************************
        $scope.getAddresss = function() {
            addressService.getAddressForaddress(addressId, $scope.getAddressForaddressCompleted, $scope.getAddressForaddressError);
        }
        $scope.getAddressForaddressCompleted = function(response) {
            $scope.AddressList = response.data;
        }
        $scope.getAddressForaddressError = function(response) {
            //
        }
        //************************ Medications Functionalities****************************************************************
        $scope.getMedicationss = function() {
            medicationsService.getMedicationsFormedications(medicationsId, $scope.getMedicationsFormedicationsCompleted, $scope.getMedicationsFormedicationsError);
        }
        $scope.getMedicationsFormedicationsCompleted = function(response) {
            $scope.MedicationsList = response.data;
        }
        $scope.getMedicationsFormedicationsError = function(response) {
            //
        }
        //************************ Problems Functionalities****************************************************************
        $scope.getProblemss = function() {
            problemsService.getProblemsForproblems(problemsId, $scope.getProblemsForproblemsCompleted, $scope.getProblemsForproblemsError);
        }
        $scope.getProblemsForproblemsCompleted = function(response) {
            $scope.ProblemsList = response.data;
        }
        $scope.getProblemsForproblemsError = function(response) {
            //
        }
        //************************ BP Functionalities****************************************************************
        $scope.getBPs = function() {
            bPService.getBPForbP(bPId, $scope.getBPForbPCompleted, $scope.getBPForbPError);
        }
        $scope.getBPForbPCompleted = function(response) {
            $scope.BPList = response.data;
        }
        $scope.getBPForbPError = function(response) {
            //
        }
        //************************ PatientHeight Functionalities****************************************************************
        $scope.getPatientHeights = function() {
            patientHeightService.getPatientHeightForpatientHeight(patientHeightId, $scope.getPatientHeightForpatientHeightCompleted, $scope.getPatientHeightForpatientHeightError);
        }
        $scope.getPatientHeightForpatientHeightCompleted = function(response) {
            $scope.PatientHeightList = response.data;
        }
        $scope.getPatientHeightForpatientHeightError = function(response) {
            //
        }
        //************************ Weight Functionalities****************************************************************
        $scope.getWeights = function() {
            weightService.getWeightForweight(weightId, $scope.getWeightForweightCompleted, $scope.getWeightForweightError);
        }
        $scope.getWeightForweightCompleted = function(response) {
            $scope.WeightList = response.data;
        }
        $scope.getWeightForweightError = function(response) {
            //
        }
        $scope.getPatientSubForms = function() {
            $scope.PatientSubFormList = [];
            $scope.PatientSubFormList.push({
                formName: 'Address',
                title: 'Address'
            },
            {
                formName: 'Medications',
                title: 'Medications'
            },
            {
                formName: 'Problems',
                title: 'Problems'
            },
            {
                formName: 'BP',
                title: 'BP'
            },
            {
                formName: 'PatientHeight',
                title: 'Patient Height'
            },
            {
                formName: 'Weight',
                title: 'Weight'
            });
            $timeout(function() {
                if ($cookies.get("tabIndex") != null) {
                    var index = $cookies.get("tabIndex");
                    $scope.onPatientSubForm($scope.PatientSubFormList[parseInt(index)], parseInt(index));
                }
                else {
                    $scope.onPatientSubForm($scope.PatientSubFormList[0], 0);
                }
            },
            100);
            return $scope.PatientSubFormList;
        }
        $scope.onPatientSubForm = function(subForm, index) {
            $scope.selected = index;
            $cookies.put("tabIndex", index);
            $scope.template = {
                'url': "App/" + subForm.formName + "/Views/" + subForm.formName + "List.html"
            };
        }
    });
})();
