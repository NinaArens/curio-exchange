using CurioExchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurioExchange.Interfaces
{
    public interface IPieceAgent
    {
        Task<ICollection<PieceModel>> RetrievePieces();

        Task<int> GetPieceIdForName(string set, string piece);
    }
}
