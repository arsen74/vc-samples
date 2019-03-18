using CustomerReviews.Core.Model;

namespace CustomerReviews.Core.Services
{
    public interface ICustomerReviewService
    {
        CustomerReview[] GetByIds(string[] ids);

        void SaveCustomerReviews(CustomerReview[] items);

        void DeleteCustomerReviews(string[] ids);

        void ApproveCustomerReview(string id);

        void RejectCustomerReview(string id);

        void AddLikeToCustomerReview(string reviewId, string userId);

        void AddDislikeToCustomerReview(string reviewId, string userId);

        double GetProductRating(string id);
    }
}
