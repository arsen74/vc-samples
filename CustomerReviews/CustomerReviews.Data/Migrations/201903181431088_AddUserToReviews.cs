namespace CustomerReviews.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddUserToReviews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReview", "UserId", c => c.String());
            DropColumn("dbo.CustomerReview", "ReviewPhotoPath");
        }

        public override void Down()
        { }
    }
}
