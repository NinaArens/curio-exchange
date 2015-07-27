using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CurioExchange.Models
{
    public class UserPieceModel : BaseModel
    {
        public UserPieceModel()
        {
            Pieces = new List<PieceModel>();
        }

        public PieceModel Piece { get; set; }

        public int Piece_Id { get; set; }

        public int[] Piece_Ids { get; set; }

        public AspNetUserModel User { get; set; }

        public string User_Id { get; set; }

        public bool Owned { get; set; }

        [DisplayName("Last updated")]
        public DateTime Added { get; set; }

        public ICollection<PieceModel> Pieces { get; set; }

        public int Amount { get; set; }

        public bool ListedByMe { get; set; }

        [Display(Name = "ID")]
        public int? OwnedID { get; set; }
    }
}