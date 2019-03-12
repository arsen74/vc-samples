angular
    .module('CustomerReviews.Web')
    .controller('CustomerReviews.Web.reviewsDetailsController', [
        '$scope',
        'CustomerReviews.WebApi',
        'platformWebApp.bladeNavigationService',
        'platformWebApp.dialogService',

        function (
            $scope,
            reviewsApi,
            bladeNavigationService,
            dialogService
        ) {
            var blade = $scope.blade;
            blade.approvePermission = 'customerReview:approve';
            blade.rejectPermission = 'customerReview:reject';

            blade.refresh = function (parentRefresh) {
                blade.isLoading = true;

                return reviewsApi.getByIds(
                    { ids: blade.currentEntityId },

                    function (dataArray) {
                        var dataItem = dataArray.shift();

                        blade.item = angular.copy(dataItem);
                        blade.currentEntity = blade.item;
                        blade.origItem = dataItem;
                        blade.isLoading = false;

                        if (parentRefresh && blade.parentBlade.refresh) {
                            blade.parentBlade.refresh();
                        }
                    },

                    function (error) {
                        bladeNavigationService.setError('Error ' + error.status, blade);
                    }
                );
            }

            blade.codeValidator = function (value) {
                return /\S/.test(value);
            };

            function canApproveOrReject() {
                return !blade.currentEntity.isActive;
            }

            function approve() {
                blade.isLoading = true;

                reviewsApi.approve(
                    { Id: blade.item.id },

                    function () {
                        blade.refresh(true);
                    },

                    function (error) {
                        bladeNavigationService.setError('Error ' + error.status, blade);
                    }
                );
            }

            function reject() {
                blade.isLoading = true;

                reviewsApi.reject(
                    { Id: blade.item.id },

                    function () {
                        $scope.bladeClose();
                        blade.parentBlade.refresh();
                    },

                    function (error) {
                        bladeNavigationService.setError('Error ' + error.status, blade);
                    }
                );
            }

            blade.formScope = null;
            $scope.setForm = function (form) { blade.formScope = form; }

            blade.headIcon = 'fa-dropbox';
            blade.title = 'Review details';

            blade.toolbarCommands = [
                {
                    name: "customerReviews.blades.review-details.commands.approve", icon: 'fa fa-check',
                    executeMethod: approve,
                    canExecuteMethod: canApproveOrReject,
                    permission: blade.approvePermission
                },
                {
                    name: "customerReviews.blades.review-details.commands.reject", icon: 'fa fa-close',
                    executeMethod: reject,
                    canExecuteMethod: canApproveOrReject,
                    permission: blade.rejectPermission
                }
            ];

            blade.refresh(false);
        }]);
