(function () {
    'use strict';
    angular.module('tokenHandlerServiceModule', ['ngCookies']).factory('tokenHandlerService', function ($q, $rootScope, $cookies) {
        return {
            request: function (config) {
                console.log($cookies.get('authToken'));
                config.headers['authToken'] = $cookies.get('authToken') || "";
                return config || $q.when(config);
            }
        };
    });
})();
