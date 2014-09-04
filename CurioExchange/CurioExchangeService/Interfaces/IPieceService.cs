using CurioExchangeService.Entities;
using CurioExchangeService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CurioExchangeService
{
    [ServiceContract]
    public interface IPieceService
    {
        [OperationContract]
        Task<ICollection<Piece>> RetrievePieces();

        [OperationContract]
        Task<ICollection<UserPiece>> RetrieveUserPieces(string userId);

        [OperationContract]
        Task<int> CreaseUserPiece(UserPiece userPiece);

        [OperationContract]
        Task DeleteUserPiece(int id);

        // todo: Exclude self when fetching possible trades
    }
}
