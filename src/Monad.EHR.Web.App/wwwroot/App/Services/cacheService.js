(function () {
    'use strict';
    angular.module('cacheServiceModule', ['angular-cache']).config(function (CacheFactoryProvider) {
        angular.extend(CacheFactoryProvider.defaults, { maxAge: 15 * 60 * 1000 });
    }).factory('cacheService', cacheService);

    cacheService.$inject = ['CacheFactory'];

    function cacheService($http) {
        var globalCache = CacheFactory('globalCache');
        var service = {
            getValue: getValue,
            putValue: putValue,
            clearCache: clearCache,
        };

        return service;

        function getValue(key) {
            return globalCache.get(key);
        }

        function putValue(key, value) {
            globalCache.put(key, value);
        }

        function clearCache() {
            globalCache = CacheFactory('globalCache');
        }
    }
})();
