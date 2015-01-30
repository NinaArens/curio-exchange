namespace CurioExchangeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPieceAddedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPieces", "Added", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPieces", "Added");
        }
    }
}
