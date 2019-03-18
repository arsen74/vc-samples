using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Web.Model;
using CustomerReviews.Web.Security;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Web.Security;

namespace CustomerReviews.Web.Controllers.Api
{
    [RoutePrefix("api/customerReviews")]
    public class CustomerReviewsController : ApiController
    {
        private readonly ICustomerReviewSearchService _customerReviewSearchService;
        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewsController()
        { }

        public CustomerReviewsController(ICustomerReviewSearchService customerReviewSearchService, ICustomerReviewService customerReviewService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
        }

        /// <summary>
        /// Return product Customer review search results
        /// </summary>
        [HttpPost]
        [Route("search")]
        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            var result = _customerReviewSearchService.SearchCustomerReviews(criteria);
            return Ok(result);
        }

        /// <summary>
        /// Get Customer Review by ID
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult GetByIds([FromUri] string[] ids)
        {
            var result = _customerReviewService.GetByIds(ids);

            return Ok(result);
        }

        /// <summary>
        ///  Create new or update existing customer review
        /// </summary>
        /// <param name="customerReviews">Customer reviews</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public IHttpActionResult Update(CustomerReview[] customerReviews)
        {
            _customerReviewService.SaveCustomerReviews(customerReviews);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete Customer Reviews by IDs
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewDelete)]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            _customerReviewService.DeleteCustomerReviews(ids);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Get Product's customer rating
        /// </summary>
        /// <param name="productId">id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("rating")]
        [ResponseType(typeof(ProductRating))]
        public IHttpActionResult GetProductRating([FromUri] string productId)
        {
            var result = _customerReviewService.GetProductRating(productId);

            return Ok(new ProductRating { ProductId = productId, Rating = result });
        }

        /// <summary>
        /// Approve Customer Review by id
        /// </summary>
        /// <param name="review">review</param>
        /// <returns></returns>
        [HttpPost]
        [Route("approve")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewApprove)]
        public IHttpActionResult Approve(CustomerReviewIdModel review)
        {
            _customerReviewService.ApproveCustomerReview(review?.Id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Reject Customer Review by id
        /// </summary>
        /// <param name="review">review</param>
        /// <returns></returns>
        [HttpPost]
        [Route("reject")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewReject)]
        public IHttpActionResult Reject(CustomerReviewIdModel review)
        {
            _customerReviewService.RejectCustomerReview(review?.Id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Like customer review
        /// </summary>
        /// <param name="appraisal">appraisal</param>
        /// <returns></returns>
        [HttpPost]
        [Route("like")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Like(CustomerReviewAppraisalModel appraisal)
        {
            _customerReviewService.AddLikeToCustomerReview(appraisal?.ReviewId, appraisal?.UserId);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Dislike customer review
        /// </summary>
        /// <param name="appraisal">appraisal</param>
        /// <returns></returns>
        [HttpPost]
        [Route("dislike")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Dislike(CustomerReviewAppraisalModel appraisal)
        {
            _customerReviewService.AddDislikeToCustomerReview(appraisal?.ReviewId, appraisal?.UserId);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
