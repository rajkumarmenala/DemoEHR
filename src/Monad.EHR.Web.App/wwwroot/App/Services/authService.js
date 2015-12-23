(function () {
    'use strict';
    angular.module('mainModule').factory('authService', ['cacheService', function (cacheService) {

        var authService = {};

        authService.isElementAccessibleForUser = function (attributes) {
            var claims = cacheService.getValue('accessRights');
            var accessCondition = attributes.elementAccess.split(" ");
            var requiredClaim = $.grep(claims, function (c) { return c.ClaimType == accessCondition; })
            .map(function (c) { return c });
            return ((requiredClaim !== null) && (requiredClaim[0].ClaimValue === 'Allowed'));
        };

        authService.isUrlAccessibleForUser = function (url) {
            var claims = cacheService.getValue('accessRights');
            if ((!claims) || (!claims.length > 0)) {
                return true;
            }
            var requiredClaim = $.grep(claims, function (c) { return c.ClaimType == url })
            .map(function (c) { return c });
            if ((!requiredClaim) || (!requiredClaim[0])) {
                return true;
            }
            return (requiredClaim[0].ClaimValue === 'Allowed');
        };
        return authService;
    }
    ])

})();
