namespace CurioExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Collection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sets", "Collection_Id", c => c.Int());
            CreateIndex("dbo.Sets", "Collection_Id");
            AddForeignKey("dbo.Sets", "Collection_Id", "dbo.Collections", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sets", "Collection_Id", "dbo.Collections");
            DropIndex("dbo.Sets", new[] { "Collection_Id" });
            DropColumn("dbo.Sets", "Collection_Id");
            DropTable("dbo.Collections");
        }
    }
}
