(function () {
    'use strict';
    angular.module('cacheServiceModule', ['angular-cache']).config(function (CacheFactoryProvider) {
        angular.extend(CacheFactoryProvider.defaults, { maxAge: 15 * 60 * 1000 });
    }).factory('cacheService', cacheService);

    cacheService.$inject = ['CacheFactory'];

    function cacheService($http) {
        var service = {
            getValue: getValue,
            setValue: setValue,
            clearCache: clearCache,
        };
        return service;

        function getValue(key) {
        }

        function setValue(key, value) {
        }

        function clearCache() {
        }
    }
})();
