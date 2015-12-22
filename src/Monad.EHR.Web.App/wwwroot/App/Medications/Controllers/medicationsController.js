(function() {
    'use strict';
    var pModule = angular.module('medicationsModule', ['homeModule', 'popUpModel', 'ngRoute', 'validation', 'validation.rule', 'smart-table']);
    pModule.config(['$routeProvider', '$validationProvider', function($routeProvider, $validationProvider) {
        $validationProvider.showSuccessMessage = false;
        $routeProvider.when("/addMedications", {
            templateUrl: '/app/Medications/Views/MedicationsAdd.html',
            controllerUrl: '/app/Medications/Controllers/medicationsController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewMedications/:medicationsId", {
            templateUrl: '/app/Medications/Views/medicationsView.html',
            controllerUrl: '/app/Medications/Controllers/medicationsController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/editMedications/:medicationsId", {
            templateUrl: '/app/Medications/Views/MedicationsEdit.html',
            controllerUrl: '/app/Medications/Controllers/medicationsController.js',
            publicAccess: true,
            sessionAccess: true
        })
    }]).config(['$httpProvider', function($httpProvider) {
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    }]).controller("medicationsController", function($scope, $injector, $routeParams, medicationsService, $cookies, $timeout) {
        var $validationProvider = $injector.get('$validation');
        $scope.initializeController = function() {
            var medicationsId = ($routeParams.medicationsId || "");
            if (medicationsId != "") {
                $scope.medicationsId = medicationsId;
                $cookies.put('medicationsId', medicationsId);
                medicationsService.getMedications(medicationsId, $scope.fetchMedicationsComplete, $scope.fetchMedicationsError);
            }
            $scope.getMedicationss();
        }
        // *********************** Get Medications data  ************* 
        $scope.getMedicationss = function() {
            medicationsService.getMedicationss($scope.fetchtMedicationssComplete, $scope.fetchtMedicationssError);
        }
        // *********************** Get Medications data  ************* 
        // *********************** Fetch Medicationss  *************
        $scope.fetchMedicationsComplete = function(response) {
            $timeout(function() {
                var medications = response.data;
                $scope.Name = medications.Name;
                $scope.Quantity = medications.Quantity;
                $scope.BeginDate = medications.BeginDate;
                $scope.EndDate = medications.EndDate;
            },
            10);
        }
        $scope.fetchMedicationssError = function(response) {
            //
        }
        // ***********************End Fetch Medications  *************
        // *********************** Fetch Medications  *************
        $scope.fetchtMedicationssComplete = function(response) {
            $scope.MedicationsList = response.data;
            var medications = response.data;
            $scope.Name = medications.Name;
            $scope.Quantity = medications.Quantity;
            $scope.BeginDate = medications.BeginDate;
            $scope.EndDate = medications.EndDate;
        }
        $scope.fetchMedicationsError = function(response) {
            //
        }
        // ***********************End Fetch Medications  *************
        $scope.addMedications = function(medications) {
            $validationProvider.validate(medications).success($scope.medicationsAddsuccess).error($scope.medicationsadderror);
        }
        $scope.medicationsAddsuccess = function() {
            var medications = $scope.createMedications();
            $scope.$broadcast('show-errors-check-validity');
            if ($scope.AddMedications.$valid) {
                medicationsService.addMedications(medications, $scope.addNewMedicationsCompleted, $scope.addError);
            }
        }
        $scope.medicationsadderror = function() {}
        //*************************** Cancel Functionalities start *******************
        $scope.addNewMedicationsCancel = function() {
            window.history.back();
            //window.location = "#/" + 'Home';
        }
        $scope.formCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewMedications';
        }
        //*************************** Cancel Functionalities end *******************
        $scope.addNewMedications = function(navigationUrl) {
            window.name = window.location.href;
            window.location = "#/" + navigationUrl;
        }
        $scope.editMedications = function(data) {
            window.name = window.location.href;
            window.location = "#/" + 'editMedications' + "/" + data;
        }
        $scope.viewMedications = function(data) {
            window.name = window.location.href;
            $cookies.put("tabIndex", 0);
            window.location = "#/" + 'viewMedications' + "/" + data;
        }
        $scope.addNewMedicationsCompleted = function(response) {
            window.history.back();
        }
        $scope.addError = function(response) {
            //
        }
        // *********************** End Add Medications  *************
        // *********************** Edit Medications  *************
        $scope.editMedicationsSaved = function(medications) {
            $validationProvider.validate(medications).success($scope.success).error($scope.error);
        }
        $scope.success = function() {
            var updateMedications = $scope.createMedications();
            if ($scope.editMedications.$valid) {
                medicationsService.editMedications(updateMedications, $scope.editMedicationsCompleted, $scope.editMedicationsError);
            }
        }
        $scope.error = function() {}
        $scope.editMedicationsCompleted = function(response) {
            window.history.back();
            //window.location = "#/" + 'viewMedications';
            //window.location=window.name;
        }
        // *********************** End Edit Medications  *************
        $scope.createMedications = function() {
            var medications = new Object();
            medications.Id = $routeParams.medicationsId;
            medications.Name = $scope.Name;
            medications.Quantity = $scope.Quantity;
            medications.BeginDate = $scope.BeginDate;
            medications.EndDate = $scope.EndDate;
            medications.PatientID = $cookies.get('patientId');
            return medications;
        }
        $scope.medicationsCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewMedications';
        }
        //********************************* Deleted Item Popup confirmation this is generic code so no need to change anythings.**************************************
        $scope.toggleModal = function(medicationsId, medicationsName) {
            $cookies.put('medicationsId', medicationsId);
            FormData = {
                'Id': medicationsId,
                'Name': medicationsName
            };
            $scope.deleteDescriptionMessage = medicationsName;
            $scope.ConfirmationMessage = "Are you sure you want to delete.?";
            $scope.showModal = true;
        };
        $scope.popupCancel = function() {
            $scope.showModal = false;
        }
        $scope.deletePopUpConfirmation = function() {
            medicationsService.deleteMedications(FormData, $scope.deleteMedicationsCompleted, $scope.fetchFormError);
            $scope.showModal = false;
        }
        $scope.deleteMedications = function(medicationsId) {
            var deleteData = {
                'Id': medicationsId
            };
            medicationsService.deleteMedications(deleteData, $scope.deleteMedicationsCompleted, $scope.fetchFormError);
        }
        $scope.deleteMedicationsCompleted = function(data) {
            $timeout(function() {
                $scope.getMedicationss();
                window.location.reload(true);
                //window.location = "#/" + 'viewMedications';
            },
            500);
        }
    });
})();
