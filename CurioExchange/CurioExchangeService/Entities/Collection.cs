using System.Collections.Generic;

namespace CurioExchangeService.Entities
{
    public class Collection : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Set> Sets { get; set; }
    }
}