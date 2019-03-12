angular.module('CustomerReviews.Web')
    .factory('CustomerReviews.WebApi', ['$resource', function ($resource) {
        return $resource('api/customerReviews', {}, {
            search: { method: 'POST', url: 'api/customerReviews/search' },
            getByIds: { method: 'GET', isArray: true },
            update: { method: 'POST' },
            getProductRating: { method: 'GET' },
            approve: { method: 'POST', url: 'api/customerReviews/approve' },
            reject: { method: 'POST', url: 'api/customerReviews/reject' }
        });
    }]);
