(function () {
    'use strict';
    angular.module('cacheServiceModule', ['angular-cache']).config(function (CacheFactoryProvider) {
        angular.extend(CacheFactoryProvider.defaults, { maxAge: 15 * 60 * 1000 });
    }).factory('cacheService', cacheService);

    cacheService.$inject = ['CacheFactory'];

    function cacheService(CacheFactory) {
        var globalCache = CacheFactory('globalCache');
        var instance;

        function createInstance() {
            
        }

        getInstance: function () {
            if (!instance) {
                instance = createInstance();
            }
            return instance;
        }

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
            console.log('For ' + key + 'Value is ' + value);
            globalCache.put(key, value);
        }

        function clearCache() {
            globalCache = CacheFactory('globalCache');
        }
    }
})();
