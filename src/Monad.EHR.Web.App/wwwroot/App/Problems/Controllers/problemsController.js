(function() {
    'use strict';
    var pModule = angular.module('problemsModule', ['homeModule', 'popUpModel', 'ngRoute', 'validation', 'validation.rule', 'smart-table']);
    pModule.config(['$routeProvider', '$validationProvider', function($routeProvider, $validationProvider) {
        $validationProvider.showSuccessMessage = false;
        $routeProvider.when("/addProblems", {
            templateUrl: '/app/Problems/Views/ProblemsAdd.html',
            controllerUrl: '/app/Problems/Controllers/problemsController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/viewProblems/:problemsId", {
            templateUrl: '/app/Problems/Views/problemsView.html',
            controllerUrl: '/app/Problems/Controllers/problemsController.js',
            publicAccess: true,
            sessionAccess: true
        }).when("/editProblems/:problemsId", {
            templateUrl: '/app/Problems/Views/ProblemsEdit.html',
            controllerUrl: '/app/Problems/Controllers/problemsController.js',
            publicAccess: true,
            sessionAccess: true
        })
    }]).config(['$httpProvider', function($httpProvider) {
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    }]).controller("problemsController", function($scope, $injector, $routeParams, problemsService, $cookies, $timeout) {
        var $validationProvider = $injector.get('$validation');
        $scope.initializeController = function() {
            var problemsId = ($routeParams.problemsId || "");
            if (problemsId != "") {
                $scope.problemsId = problemsId;
                $cookies.put('problemsId', problemsId);
                problemsService.getProblems(problemsId, $scope.fetchProblemsComplete, $scope.fetchProblemsError);
            }
            $scope.getProblemss();
        }
        // *********************** Get Problems data  ************* 
        $scope.getProblemss = function() {
            problemsService.getProblemss($scope.fetchtProblemssComplete, $scope.fetchtProblemssError);
        }
        // *********************** Get Problems data  ************* 
        // *********************** Fetch Problemss  *************
        $scope.fetchProblemsComplete = function(response) {
            $timeout(function() {
                var problems = response.data;
                $scope.Description = problems.Description;
                $scope.Date = problems.Date;
            },
            10);
        }
        $scope.fetchProblemssError = function(response) {
            //
        }
        // ***********************End Fetch Problems  *************
        // *********************** Fetch Problems  *************
        $scope.fetchtProblemssComplete = function(response) {
            $scope.ProblemsList = response.data;
            var problems = response.data;
            $scope.Description = problems.Description;
            $scope.Date = problems.Date;
        }
        $scope.fetchProblemsError = function(response) {
            //
        }
        // ***********************End Fetch Problems  *************
        $scope.addProblems = function(problems) {
            $validationProvider.validate(problems).success($scope.problemsAddsuccess).error($scope.problemsadderror);
        }
        $scope.problemsAddsuccess = function() {
            var problems = $scope.createProblems();
            $scope.$broadcast('show-errors-check-validity');
            if ($scope.AddProblems.$valid) {
                problemsService.addProblems(problems, $scope.addNewProblemsCompleted, $scope.addError);
            }
        }
        $scope.problemsadderror = function() {}
        //*************************** Cancel Functionalities start *******************
        $scope.addNewProblemsCancel = function() {
            window.history.back();
            //window.location = "#/" + 'Home';
        }
        $scope.formCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewProblems';
        }
        //*************************** Cancel Functionalities end *******************
        $scope.addNewProblems = function(navigationUrl) {
            window.name = window.location.href;
            window.location = "#/" + navigationUrl;
        }
        $scope.editProblems = function(data) {
            window.name = window.location.href;
            window.location = "#/" + 'editProblems' + "/" + data;
        }
        $scope.viewProblems = function(data) {
            window.name = window.location.href;
            $cookies.put("tabIndex", 0);
            window.location = "#/" + 'viewProblems' + "/" + data;
        }
        $scope.addNewProblemsCompleted = function(response) {
            window.history.back();
        }
        $scope.addError = function(response) {
            //
        }
        // *********************** End Add Problems  *************
        // *********************** Edit Problems  *************
        $scope.editProblemsSaved = function(problems) {
            $validationProvider.validate(problems).success($scope.success).error($scope.error);
        }
        $scope.success = function() {
            var updateProblems = $scope.createProblems();
            if ($scope.editProblems.$valid) {
                problemsService.editProblems(updateProblems, $scope.editProblemsCompleted, $scope.editProblemsError);
            }
        }
        $scope.error = function() {}
        $scope.editProblemsCompleted = function(response) {
            window.history.back();
            //window.location = "#/" + 'viewProblems';
            //window.location=window.name;
        }
        // *********************** End Edit Problems  *************
        $scope.createProblems = function() {
            var problems = new Object();
            problems.Id = $routeParams.problemsId;
            problems.Description = $scope.Description;
            problems.Date = $scope.Date;
            problems.PatientID = $cookies.get('patientId');
            return problems;
        }
        $scope.problemsCancel = function() {
            window.history.back();
            // window.location = "#/" + 'viewProblems';
        }
        //********************************* Deleted Item Popup confirmation this is generic code so no need to change anythings.**************************************
        $scope.toggleModal = function(problemsId, problemsName) {
            $cookies.put('problemsId', problemsId);
            FormData = {
                'Id': problemsId,
                'Name': problemsName
            };
            $scope.deleteDescriptionMessage = problemsName;
            $scope.ConfirmationMessage = "Are you sure you want to delete.?";
            $scope.showModal = true;
        };
        $scope.popupCancel = function() {
            $scope.showModal = false;
        }
        $scope.deletePopUpConfirmation = function() {
            problemsService.deleteProblems(FormData, $scope.deleteProblemsCompleted, $scope.fetchFormError);
            $scope.showModal = false;
        }
        $scope.deleteProblems = function(problemsId) {
            var deleteData = {
                'Id': problemsId
            };
            problemsService.deleteProblems(deleteData, $scope.deleteProblemsCompleted, $scope.fetchFormError);
        }
        $scope.deleteProblemsCompleted = function(data) {
            $timeout(function() {
                $scope.getProblemss();
                window.location.reload(true);
                //window.location = "#/" + 'viewProblems';
            },
            500);
        }
    });
})();
