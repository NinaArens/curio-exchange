using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurioExchangeService.Entities
{
    public class UserSet : Entity
    {
        public virtual Set Set { get; set; }

        [ForeignKey("Set")]
        public int Set_Id { get; set; }

        public virtual AspNetUser User { get; set; }

        [ForeignKey("User")]
        public string User_Id { get; set; }

        public bool Owned { get; set; }

        public DateTime Added { get; set; }

        public int? OwnedID { get; set; }
    }
}