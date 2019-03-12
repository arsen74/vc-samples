using System;
using System.Linq;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Data.Model;
using CustomerReviews.Data.Repositories;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace CustomerReviews.Data.Services
{
    public class CustomerReviewService : ServiceBase, ICustomerReviewService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;
        private readonly ICustomerReviewRatingCalculator _ratingCalculator;

        public CustomerReviewService(Func<ICustomerReviewRepository> repositoryFactory, ICustomerReviewRatingCalculator ratingCalculator)
        {
            _repositoryFactory = repositoryFactory;
            _ratingCalculator = ratingCalculator;
        }

        public CustomerReview[] GetByIds(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                return repository.GetByIds(ids).Select(x => x.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance())).ToArray();
            }
        }

        public CustomerReview[] GetByProductId(string id)
        {
            using (var repository = _repositoryFactory())
            {
                return repository.GetByProductId(id).Select(x => x.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance())).ToArray();
            }
        }

        public void SaveCustomerReviews(CustomerReview[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = _repositoryFactory())
            {
                using (var changeTracker = GetChangeTracker(repository))
                {
                    var alreadyExistEntities = repository.GetByIds(items.Where(m => !m.IsTransient()).Select(x => x.Id).ToArray());
                    foreach (var derivativeContract in items)
                    {
                        var sourceEntity = AbstractTypeFactory<CustomerReviewEntity>.TryCreateInstance().FromModel(derivativeContract, pkMap);
                        var targetEntity = alreadyExistEntities.FirstOrDefault(x => x.Id == sourceEntity.Id);
                        if (targetEntity != null)
                        {
                            changeTracker.Attach(targetEntity);
                            sourceEntity.Patch(targetEntity);
                        }
                        else
                        {
                            repository.Add(sourceEntity);
                        }
                    }

                    CommitChanges(repository);
                    pkMap.ResolvePrimaryKeys();
                }
            }
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                repository.DeleteCustomerReviews(ids);
                CommitChanges(repository);
            }
        }

        public void ApproveCustomerReview(string id)
        {
            using (var repository = _repositoryFactory())
            {
                var item = repository.GetById(id);
                if (item != null)
                {
                    item.IsActive = true;
                }

                repository.Save(item);

                CommitChanges(repository);
            }
        }

        public void RejectCustomerReview(string id)
        {
            DeleteCustomerReviews(new[] { id });
        }

        public void AddLikeToCustomerReview(string id)
        {
            using (var repository = _repositoryFactory())
            {
                var item = repository.GetById(id);
                if (item != null)
                {
                    item.LikeCount = item.LikeCount + 1;
                }

                repository.Save(item);

                CommitChanges(repository);
            }
        }

        public void AddDislikeToCustomerReview(string id)
        {
            using (var repository = _repositoryFactory())
            {
                var item = repository.GetById(id);
                if (item != null)
                {
                    item.DislikeCount = item.DislikeCount + 1;
                }

                repository.Save(item);

                CommitChanges(repository);
            }
        }

        public double GetProductRating(string id)
        {
            var reviews = GetByProductId(id);

            return _ratingCalculator.CalculateRating(reviews.Where(p => p.IsActive));
        }
    }
}
