using CurioExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurioExchange.Interfaces
{
    public interface IUserPieceAgent
    {
        Task<ICollection<UserPieceModel>> RetrieveUserPieces(string userId, string name = null);

        Task<int> CreaseUserPiece(UserPieceModel userPiece);

        Task<IEnumerable<int>> CreaseUserPieces(UserPieceModel userPiece);

        Task DeleteUserPiece(int id);

        Task DeleteUserPieces(string userId, bool owned);

        Task<ICollection<UserPieceModel>> RetrieveTradesWanted(string userId);

        Task<ICollection<UserPieceModel>> RetrieveTradesOwned(string userId);

        Task RefreshUserPiece(int id);
    }
}
