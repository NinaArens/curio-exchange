namespace CurioExchangeService.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(nullable: false));
        }
    }
}
