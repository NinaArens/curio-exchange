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
    public interface IUserSetService
    {
        [OperationContract]
        Task<ICollection<UserSet>> RetrieveUserSets(string userId);

        [OperationContract]
        Task<ICollection<UserSet>> RetrieveUserSetsWanted(string userId);

        [OperationContract]
        Task<ICollection<UserSet>> RetrieveUserSetsOwned(string userId);

        [OperationContract]
        Task<int> CreaseUserSet(UserSet userSet);

        [OperationContract]
        Task DeleteUserSet(int id);

        [OperationContract]
        Task DeleteUserSets(string userId, bool owned);

        [OperationContract]
        Task<ICollection<UserSet>> RetrieveTradesWanted(string userId);

        [OperationContract]
        Task<ICollection<UserSet>> RetrieveTradesOwned(string userId);

        [OperationContract]
        Task RefreshUserSet(int id);

        [OperationContract]
        Task<bool> WantedBy(int setId, string name);

        [OperationContract]
        Task<bool> OwnedBy(int setId, string name);
    }
}
