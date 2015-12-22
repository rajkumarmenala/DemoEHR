(function () {
    'use strict';
    angular.module('mainModule').directive('elementAccess', ['cacheService', 'authService', function (cacheService, authService) {

        var elementAccess = {};
        return {
            restrict: 'A',
            link: function (scope, element, attributes) {
                console.log(authService);
                if (!authService.isElementAccessibleForUser(attributes)) {
                    angular.forEach(element.children(), function (child) {
                        child && child.remove && child.remove();
                    });
                    element && element.remove && element.remove();
                }
            }
        }
    }
    ])
})();