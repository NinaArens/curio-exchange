using CurioExchangeService.Entities;
using System.Data.Entity;

namespace CurioExchangeService
{
    public class CurioExchangeContext : DbContext
    {
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<UserPiece> UserPieces { get; set; }
        public DbSet<UserSet> UserSets { get; set; }
    }
}