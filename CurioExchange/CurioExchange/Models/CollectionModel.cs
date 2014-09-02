using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurioExchange.Models
{
    public class CollectionModel : BaseModel
    {
        public string Name { get; set; }

        public ICollection<SetModel> Sets { get; set; }
    }
}