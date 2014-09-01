using CurioExchange.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CurioExchange
{
    public class CurioExchangeContext : DbContext
    {
        public DbSet<Piece> Pieces { get; set; }
    }
}