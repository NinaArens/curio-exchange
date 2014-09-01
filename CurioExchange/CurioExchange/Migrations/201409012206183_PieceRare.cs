namespace CurioExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PieceRare : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pieces", "Rare", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pieces", "Rare");
        }
    }
}
