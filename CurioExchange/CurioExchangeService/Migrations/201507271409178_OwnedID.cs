namespace CurioExchangeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OwnedID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPieces", "OwnedID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPieces", "OwnedID");
        }
    }
}
