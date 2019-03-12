using System;
using System.Collections.Generic;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;

namespace CustomerReviews.Data.Services
{
    public sealed class AvarageCustomerReviewRatingCalculator : ICustomerReviewRatingCalculator
    {
        public double CalculateRating(IEnumerable<CustomerReview> ratings)
        {
            double values = 0;
            double weights = 0;

            double currentWeight = 0;
            foreach (var rating in ratings)
            {
                currentWeight = (rating.LikeCount + 1) / (double)(rating.DislikeCount + 1);
                weights += currentWeight;
                values += rating.Rating * currentWeight;
            }

            return weights > 0 ?
                Math.Round(values / weights, 2, MidpointRounding.AwayFromZero) :
                0;
        }
    }
}
