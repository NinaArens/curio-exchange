using CurioExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurioExchange.Interfaces
{
    public interface IPieceAgent
    {
        Task<ICollection<PieceModel>> RetrievePieces();

        Task<ICollection<UserPieceModel>> RetrieveUserPieces(string userId);

        Task<int> CreaseUserPiece(UserPieceModel userPiece);

        Task<IEnumerable<int>> CreaseUserPieces(UserPieceModel userPiece);

        Task DeleteUserPiece(int id);

        Task<ICollection<UserPieceModel>> RetrieveTradesWanted(string userId);

        Task<ICollection<UserPieceModel>> RetrieveTradesOwned(string userId);

        Task<int> GetPieceIdForName(string set, string piece);
    }
}
