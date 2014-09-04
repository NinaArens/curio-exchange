using CurioExchangeService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurioExchangeService.Entities
{
    public class UserPiece : Entity
    {
        public virtual Piece Piece { get; set; }

        public virtual AspNetUser User { get; set; }

        public bool Owned { get; set; }
    }
}