namespace CustomerReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using CustomerReviews.Data.Model;
    using CustomerReviews.Data.Repositories;

    public sealed class Configuration : DbMigrationsConfiguration<CustomerReviewRepository>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations";
        }

        protected override void Seed(CustomerReviews.Data.Repositories.CustomerReviewRepository context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var now = DateTime.UtcNow;

            context.AddOrUpdate(
                new CustomerReviewEntity
                {
                    Id = "1",
                    ProductId = "6e7a31c35c814fb389dc2574aa142b63",
                    CreatedDate = now,
                    CreatedBy = "initial data seed",
                    AuthorNickname = "Arsen",
                    Content = "Super!",
                    Rating = 5,
                    LikeCount = 2
                });
            context.AddOrUpdate(
                new CustomerReviewEntity
                {
                    Id = "2",
                    ProductId = "6e7a31c35c814fb389dc2574aa142b63",
                    CreatedDate = now,
                    CreatedBy = "initial data seed",
                    AuthorNickname = "Arsen",
                    Content = "So so",
                    Rating = 3,
                    DislikeCount = 1
                });
            context.AddOrUpdate(
                new CustomerReviewEntity
                {
                    Id = "3",
                    ProductId = "6e7a31c35c814fb389dc2574aa142b63",
                    CreatedDate = now,
                    CreatedBy = "initial data seed",
                    AuthorNickname = "Arsen",
                    Content = "Liked that",
                    Rating = 4,
                    LikeCount = 1,
                    DislikeCount = 2
                });

            context.SaveChanges();
        }
    }
}
