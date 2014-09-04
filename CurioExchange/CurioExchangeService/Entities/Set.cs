using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurioExchangeService.Entities
{
    public class Set : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Piece> Pieces { get; set; }

        public virtual Collection Collection { get; set; }
    }
}