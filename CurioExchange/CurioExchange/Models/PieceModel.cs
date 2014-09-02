using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurioExchange.Models
{
    public class PieceModel : BaseModel
    {
        public string Name { get; set; }

        public virtual SetModel Set { get; set; }

        public bool Rare { get; set; }
    }
}