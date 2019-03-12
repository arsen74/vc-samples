namespace CustomerReviews.Web.Security
{
    public static class PredefinedPermissions
    {
        public const string CustomerReviewRead = "customerReview:read",
                    CustomerReviewUpdate = "customerReview:update",
                    CustomerReviewDelete = "customerReview:delete",
                    CustomerReviewApprove = "customerReview:approve",
                    CustomerReviewReject = "customerReview:reject",
                    CustomerReviewValuation = "customerReview:valuation";
    }
}
