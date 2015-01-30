using CurioExchangeService.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CurioExchangeService.Entities
{
    public class UserPiece : Entity
    {
        public virtual Piece Piece { get; set; }

        [ForeignKey("Piece")]
        public int Piece_Id { get; set; }

        public virtual AspNetUser User { get; set; }

        [ForeignKey("User")]
        public string User_Id { get; set; }

        public bool Owned { get; set; }

        public DateTime Added { get; set; }
    }
}