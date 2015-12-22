(function () {
    'use strict';
    angular.module('cacheServiceModule', ['angular-cache']).config(function (CacheFactoryProvider) {
        angular.extend(CacheFactoryProvider.defaults, { maxAge: 15 * 60 * 1000 });
    }).factory('cacheService', function (CacheFactory) {

        var cacheService = {};
        cacheService.$inject = ['CacheFactory'];
        cacheService.globalCache = CacheFactory('globalCache');

        cacheService.getValue = function (key) {
            return cacheService.globalCache.get(key);
        }

        cacheService.putValue = function (key, value) {
            cacheService.globalCache.put(key, value);
        }

        cacheService.clearCache = function () {
            //this = CacheFactory('globalCache');
        }
        return cacheService;
    });

})();


