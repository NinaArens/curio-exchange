using CurioExchangeService.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CurioExchangeService
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Task<ICollection<AspNetUser>> RetrieveUsers();

        [OperationContract]
        Task<AspNetUser> RetrieveUserById(string userId);
    }
}
