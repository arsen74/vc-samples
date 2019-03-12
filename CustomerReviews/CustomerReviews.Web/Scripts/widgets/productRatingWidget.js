angular
    .module('CustomerReviews.Web')
    .controller('CustomerReviews.Web.productRatingWidgetController', [
        '$scope',
        'CustomerReviews.WebApi',
        'platformWebApp.bladeNavigationService',

        function (
            $scope,
            reviewsApi
        ) {
            var filter = {};

            function refresh() {
                $scope.loading = true;

                reviewsApi.getProductRating(filter, function (data) {
                    var sumRating = 0;

                    angular.forEach(data.results, function (review) {
                        sumRating += review.rating;
                    });

                    $scope.rating = sumRating > 0
                        ? sumRating
                        : null;

                    $scope.loading = false;
                });
            }

            $scope.$watch("blade.itemId", function (id) {
                filter.productId = id;

                if (id) refresh();
            });
        }]);
