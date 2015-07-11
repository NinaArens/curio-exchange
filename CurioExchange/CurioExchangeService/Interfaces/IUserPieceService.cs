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
    public interface IUserPieceService
    {
        [OperationContract]
        Task<ICollection<UserPiece>> RetrieveUserPieces(string userId);

        [OperationContract]
        Task<ICollection<UserPiece>> RetrieveUserPiecesWanted(string userId);

        [OperationContract]
        Task<ICollection<UserPiece>> RetrieveUserPiecesOwned(string userId);

        [OperationContract]
        Task<int> CreaseUserPiece(UserPiece userPiece);

        [OperationContract]
        Task DeleteUserPiece(int id);

        [OperationContract]
        Task DeleteUserPieces(string userId, bool owned);

        [OperationContract]
        Task<ICollection<UserPiece>> RetrieveTradesWanted(string userId);

        [OperationContract]
        Task<ICollection<UserPiece>> RetrieveTradesOwned(string userId);

        [OperationContract]
        Task RefreshUserPiece(int id);

        [OperationContract]
        Task<bool> WantedBy(int pieceId, string name);

        [OperationContract]
        Task<bool> OwnedBy(int pieceId, string name);
    }
}
