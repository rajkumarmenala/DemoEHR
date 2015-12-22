(function () {
    'use strict';
    angular.module('mainModule').directive('elementAccess', ['cacheService', function (cacheService) {

        var elementAccess = {};
        return {
            restrict: 'A',
            link: function (scope, element, attributes) {
                var hasAccess = false;
                var claims = cacheService.getValue('accessRights');
                var accessCondition = attributes.elementAccess.split(" ");
                var requiredClaim = $.grep(claims, function (c) { return c.ClaimType == accessCondition; })
                .map(function (c) { return c });
                console.log(requiredClaim[0].ClaimValue);
                console.log(element);
                if (requiredClaim[0].ClaimValue !== 'Allowed') {
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