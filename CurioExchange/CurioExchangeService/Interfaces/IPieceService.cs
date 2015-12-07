using CurioExchangeService.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CurioExchangeService
{
    [ServiceContract]
    public interface IPieceService
    {
        [OperationContract]
        Task<ICollection<Piece>> RetrievePieces();

        [OperationContract]
        Task<int> GetPieceIdForName(string set, string piece);
    }
}
