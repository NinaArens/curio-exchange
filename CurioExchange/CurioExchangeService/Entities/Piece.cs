using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurioExchange.Entities
{
    public class Piece : Entity
    {
        public string Name { get; set; }

        public virtual Set Set { get; set; }

        public bool Rare { get; set; }
    }
}