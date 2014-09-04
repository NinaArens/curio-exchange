using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurioExchange.Models
{
    public class UserPieceModel : BaseModel
    {
        public PieceModel Piece { get; set; }

        public AspNetUserModel User { get; set; }

        public bool Owned { get; set; }
    }
}