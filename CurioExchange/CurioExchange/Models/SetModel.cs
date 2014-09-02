using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurioExchange.Models
{
    public class SetModel : BaseModel
    {
        public string Name { get; set; }

        public ICollection<PieceModel> Pieces { get; set; }

        public CollectionModel Collection { get; set; }
    }
}