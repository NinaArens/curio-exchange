using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CurioExchange.Models
{
    public class UserSetModel : BaseModel
    {
        public UserSetModel()
        {
            Sets = new List<SetModel>();
        }

        public SetModel Set { get; set; }

        public int Set_Id { get; set; }

        public int[] Set_Ids { get; set; }

        public AspNetUserModel User { get; set; }

        public string User_Id { get; set; }

        public bool Owned { get; set; }

        [DisplayName("Last updated")]
        public DateTime Added { get; set; }

        public ICollection<SetModel> Sets { get; set; }

        public int Amount { get; set; }

        public bool ListedByMe { get; set; }

        [Display(Name = "ID")]
        public int? OwnedID { get; set; }
    }
}