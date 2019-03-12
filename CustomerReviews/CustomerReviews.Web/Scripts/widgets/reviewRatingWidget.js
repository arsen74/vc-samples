angular
    .module('CustomerReviews.Web')
    .controller('CustomerReviews.Web.reviewRatingWidgetController', [
        '$scope',

        function ($scope) {
            $scope.rating = $scope.blade.currentEntity.productRating;
        }
    ]);
