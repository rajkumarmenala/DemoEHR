(function () {
    'use strict';
    var image = new Object();
    var userModule = angular
      .module('userModule', ['interceptorServiceModule', 'userServiceModule', 'applicationServiceModule', 'alertsServiceModule', 'naif.base64',
          'ngRoute', 'ngResource', 'ngCookies', 'ngSanitize'
          , 'validation', 'validation.rule']);

    userModule.config(['$routeProvider', '$httpProvider', '$validationProvider', function ($routeProvider, $httpProvider, $validationProvider, interceptorService) {
        $validationProvider.showSuccessMessage = false;
        $routeProvider
            .when("/userProfile", {
                templateUrl: '/app/Accounts/Views/userProfile.html',
                controllerUrl: '/app/Accounts/Controllers/userController.js',
                publicAccess: true,
                sessionAccess: true
            })
            .when("/editUserProfile", {
                templateUrl: '/app/Accounts/Views/editUserProfile.html',
                controllerUrl: '/app/Accounts/Controllers/userController.js',
                publicAccess: true,
                sessionAccess: true
            })
        $httpProvider.interceptors.push('interceptorService');

        //Custom validation
        $validationProvider
            .setExpression({
                zipFormatter: function (value, scope, element, attrs, param) {
                    var regexp = /^\d{5}(-\d{4})?$/;
                    if (scope.$parent.Zip == null || scope.$parent.Zip == "") {
                        return true;
                    }
                    return (regexp.test(scope.$parent.Zip));
                }
            })

       .setDefaultMsg({
           zipFormatter: {
               error: 'US format zip code(e.g., "94105-0011" or "94105")'
           }
       })

            .setExpression({
                noSpecialCharectorForUser: function (value, scope, element, attrs, param) {
                    var regexp = /[-!$%^&*()_+|~=`\\@#{}\[\]:";'<>?,\/]/;
                    return (!regexp.test(scope.$parent.Designation))
                }
            })
       .setDefaultMsg({
           noSpecialCharectorForUser: {
               error: 'Except dot and space,no special charectors are allowed.'
           }
       })
         .setExpression({
             noSpecialCharectorForFirstName: function (value, scope, element, attrs, param) {
                 var regexp = /[-!$%^&*()_+|~=`\\@#{}\[\]:";'<>?,\/]/;
                 return (!regexp.test(scope.$parent.FirstName))
             }
         })
       .setDefaultMsg({
           noSpecialCharectorForFirstName: {
               error: 'Except dot and space,no special charectors are allowed.'
           }
       })
        .setExpression({
            noSpecialCharectorForLastName: function (value, scope, element, attrs, param) {
                var regexp = /[-!$%^&*()_+|~=`\\@#{}\[\]:";'<>?,\/]/;
                return (!regexp.test(scope.$parent.LastName))
            }
        })
      .setDefaultMsg({
          noSpecialCharectorForLastName: {
              error: 'Except dot and space,no special charectors are allowed.'
          }
      });



    }]);
    userModule.config(['$httpProvider', function ($httpProvider) {

        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    }]);
    userModule.controller("userController", ['$scope', '$injector', '$rootScope', '$resource', 'userService', '$cookies', 'applicationService', 'alertsService', '$timeout', '$location', function ($scope, $injector, $rootScope, $resource, userService, $cookies, applicationService, alertsService, $timeout, $location) {
        var $validationProvider = $injector.get('$validation');
        $scope.initializeController = function () {
            $scope.UserName = $cookies.get('currentUserName');
            $scope.FirstName = "";
            $scope.LastName = "";
            $scope.EmailAddress = "";
            $scope.Designation = "";
            $scope.AddressLine1 = "";
            $scope.AddressLine2 = "";
            $scope.Zip = "";
            $scope.City = "";
            $scope.State = "";
            $scope.User = {};
            $scope.getUserProfile();
        }

        $scope.getUserProfile = function () {
            userService.showUserProfile($scope.UserName, $scope.getUserProfileCompleted, $scope.getUserProfileError);
        }

        $scope.getUserProfileCompleted = function (response) {
            $scope.User = response.data;
            $cookies.put('currentUserId', response.data.Id);
            var applicationUser = response.data;
            $scope.FirstName = applicationUser.FirstName;
            $scope.LastName = applicationUser.LastName;
            $scope.EmailAddress = applicationUser.EmailAddress;
            $scope.Designation = applicationUser.Designation;
            $scope.AddressLine1 = applicationUser.AddressLine1;
            $scope.AddressLine2 = applicationUser.AddressLine2;
            $scope.Zip = applicationUser.Zip;
            $scope.$parent.User.FirstName = applicationUser.FirstName;
            $scope.$parent.User.LastName = applicationUser.LastName;
            $scope.City = applicationUser.City;
            $scope.State = applicationUser.State;
            if (applicationUser.ProfilePicture != null) {
                $scope.getUploadedImage(applicationUser.ProfilePicture);
            }

        }

        $scope.getUserProfileError = function (response) {
            $scope.clearValidationErrors();
        }

        $scope.clearValidationErrors = function () {
            $scope.UserNameInputError = false;
            $scope.PasswordInputError = false;
            $scope.ConfirmPasswordInputError = false;
        }

        $scope.editUserRecord = function () {
            window.location = "#/editUserProfile";
        }

        $scope.editUserProfileCancel = function () {
            window.location = "#/userProfile";
        }

        $scope.saveUserProfile = function (form) {
            $validationProvider.validate(form)
               .success($scope.submitSuccess)
               .error($scope.submitError);

        }

        $scope.submitSuccess = function () {
            userService.editUserProfile($scope.createUser(), $scope.saveUserProfileCompleted, $scope.saveUserProfileError);
        }

        $scope.submitError = function (data) {
        }

        $scope.saveUserProfileCompleted = function () {
            window.location.reload(true);
            $location.path('userProfile');
        }

        $scope.saveUserProfileError = function () {

            $scope.clearValidationErrors();
        }
        $scope.createUser = function () {
            var user = new Object();
            user.Id = $cookies.get('currentUserId');
            user.UserName = $cookies.get('currentUserName');
            user.FirstName = $scope.FirstName;
            user.LastName = $scope.LastName;
            user.EmailAddress = $scope.EmailAddress;
            user.Designation = $scope.Designation;
            user.AddressLine1 = $scope.AddressLine1;
            user.AddressLine2 = $scope.AddressLine2;
            user.Zip = $scope.Zip;
            user.City = $scope.City;
            user.State = $scope.State;
            user.ProfilePicture = $cookies.get('profilePicture');
            $scope.uploadImage();
            return user;
        }

        //Upload image functionalities ********************************************************
        //start
        $scope.createImage = function (fileObj) {
            image.FileName = $cookies.get('currentUserName').replace(/\./g, '_') + '_' + fileObj.name;
            image.FileSize = fileObj.filesize;
            image.FileType = fileObj.filetype;
            $cookies.put('profilePicture', image.FileName);
            return image;
        }

        $scope.uploadImage = function () {
            userService.uploadImage(image, $scope.getFileUploadedCompleted, $scope.getUserProfileError);
        }

        $scope.getFileUploadedCompleted = function () {
        }

        $scope.onLoad = function (e, reader, file, fileList, fileOjects, fileObj) {
            $timeout(function () {
                $scope.trimImage(file, fileObj.base64);
                $scope.createImage(file);
                $scope.isImagePreview = true;
            }, 100);

        };



        $scope.getUploadedImage = function (imageName) {
            userService.getUploadedImage(imageName, $scope.getUploadedImageCompleted, $scope.getUserProfileError);
        }

        $scope.getUploadedImageCompleted = function (response) {
            $scope.imageData = response.data;
            if (response != null) {
                $scope.isImagePreview = true;
                $scope.$parent.User.imageData = response.data;
            }
        }

        $scope.trimImage = function (file, data) {

            var img = document.getElementById("photo-id");
            $scope.imageData = data;
            var reader = new FileReader();
            reader.onload = function (e) {
                var canvas = document.createElement("canvas");
                var ctx = canvas.getContext("2d");
                ctx.drawImage(img, 0, 0);
                var MAX_WIDTH = 75;
                var MAX_HEIGHT = 75;
                var width = img.width;
                var height = img.height;

                if (width > height) {
                    if (width > MAX_WIDTH) {
                        height *= MAX_WIDTH / width;
                        width = MAX_WIDTH;
                    }
                } else {
                    if (height > MAX_HEIGHT) {
                        width *= MAX_HEIGHT / height;
                        height = MAX_HEIGHT;
                    }
                }
                canvas.width = width;
                canvas.height = height;
                var ctx = canvas.getContext("2d");
                ctx.drawImage(img, 0, 0, width, height);

                var dataurl = canvas.toDataURL("image/png");
                document.getElementById('photo-id').src = dataurl;
                $scope.isImagePreview = true;
                image.Base64 = dataurl.replace(/data:image\/png;base64,/g, '');
            }
            $scope.trimeImagefile = file;
            var data = reader.readAsDataURL(file);


        }

        //End *******************************************************************************

    }])
})();


