using System.Collections.Generic;
using CustomerReviews.Core.Model;

namespace CustomerReviews.Core.Services
{
    public interface ICustomerReviewRatingCalculator
    {
        double CalculateRating(IEnumerable<CustomerReview> ratings);
    }
}
