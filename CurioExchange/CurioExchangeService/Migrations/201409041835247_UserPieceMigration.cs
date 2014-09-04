namespace CurioExchangeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPieceMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPieces", "Piece_Id", "dbo.Pieces");
            DropIndex("dbo.UserPieces", new[] { "Piece_Id" });
            AlterColumn("dbo.UserPieces", "Piece_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.UserPieces", "Piece_Id");
            AddForeignKey("dbo.UserPieces", "Piece_Id", "dbo.Pieces", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPieces", "Piece_Id", "dbo.Pieces");
            DropIndex("dbo.UserPieces", new[] { "Piece_Id" });
            AlterColumn("dbo.UserPieces", "Piece_Id", c => c.Int());
            CreateIndex("dbo.UserPieces", "Piece_Id");
            AddForeignKey("dbo.UserPieces", "Piece_Id", "dbo.Pieces", "Id");
        }
    }
}
