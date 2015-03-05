using CurioExchange.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CurioExchange.ViewModels
{
    public class UserPieceViewModel
    {
        [DisplayName("Player")]
        public string Username { get; set; }

        public List<UserPieceModel> OwnedPieces { get; set; }

        public List<UserPieceModel> WantedPieces { get; set; }

        public UserPieceViewModel()
        {
            OwnedPieces = new List<UserPieceModel>();
            WantedPieces = new List<UserPieceModel>();
        }
    }
}