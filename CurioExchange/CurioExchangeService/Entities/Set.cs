using System.Collections.Generic;

namespace CurioExchangeService.Entities
{
    public class Set : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Piece> Pieces { get; set; }

        public virtual Collection Collection { get; set; }
    }
}