namespace CurioExchangeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
            
            CreateTable(
                "dbo.Sets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Collection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Collections", t => t.Collection_Id)
                .Index(t => t.Collection_Id);
            
            CreateTable(
                "dbo.Pieces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rare = c.Boolean(nullable: false),
                        Set_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sets", t => t.Set_Id)
                .Index(t => t.Set_Id);
            
            CreateTable(
                "dbo.UserPieces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Owned = c.Boolean(nullable: false),
                        Piece_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pieces", t => t.Piece_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Piece_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPieces", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPieces", "Piece_Id", "dbo.Pieces");
            DropForeignKey("dbo.Pieces", "Set_Id", "dbo.Sets");
            DropForeignKey("dbo.Sets", "Collection_Id", "dbo.Collections");
            DropIndex("dbo.UserPieces", new[] { "User_Id" });
            DropIndex("dbo.UserPieces", new[] { "Piece_Id" });
            DropIndex("dbo.Pieces", new[] { "Set_Id" });
            DropIndex("dbo.Sets", new[] { "Collection_Id" });
            DropTable("dbo.UserPieces");
            DropTable("dbo.Pieces");
            DropTable("dbo.Sets");
            DropTable("dbo.Collections");
        }
    }
}
