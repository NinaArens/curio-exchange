using System.Collections.Generic;

namespace CurioExchange.Models
{
    public class CollectionModel : BaseModel
    {
        public string Name { get; set; }

        public ICollection<SetModel> Sets { get; set; }
    }
}