namespace CurioExchangeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Set_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Owned = c.Boolean(nullable: false),
                        Added = c.DateTime(nullable: false),
                        OwnedID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sets", t => t.Set_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Set_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSets", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSets", "Set_Id", "dbo.Sets");
            DropIndex("dbo.UserSets", new[] { "User_Id" });
            DropIndex("dbo.UserSets", new[] { "Set_Id" });
            DropTable("dbo.UserSets");
        }
    }
}
