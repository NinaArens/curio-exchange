using System.Collections.Generic;

namespace CurioExchange.Models
{
    public class SetModel : BaseModel
    {
        public string Name { get; set; }

        public ICollection<PieceModel> Pieces { get; set; }

        public CollectionModel Collection { get; set; }
    }
}