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
                    $scope.rating = data.rating > 0
                        ? data.rating
                        : null;

                    $scope.loading = false;
                });
            }

            $scope.$watch("blade.itemId", function (id) {
                filter.productId = id;

                if (id) refresh();
            });
        }]);
