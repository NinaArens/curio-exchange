using CurioExchangeService.Entities;
using CurioExchangeService.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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