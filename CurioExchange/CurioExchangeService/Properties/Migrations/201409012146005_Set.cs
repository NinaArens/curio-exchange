namespace CurioExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Set : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Pieces", "Set_Id", c => c.Int());
            CreateIndex("dbo.Pieces", "Set_Id");
            AddForeignKey("dbo.Pieces", "Set_Id", "dbo.Sets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pieces", "Set_Id", "dbo.Sets");
            DropIndex("dbo.Pieces", new[] { "Set_Id" });
            DropColumn("dbo.Pieces", "Set_Id");
            DropTable("dbo.Sets");
        }
    }
}
