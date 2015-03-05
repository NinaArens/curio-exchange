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
    public interface IUserService
    {
        [OperationContract]
        Task<ICollection<AspNetUser>> RetrieveUsers();

        [OperationContract]
        Task<AspNetUser> RetrieveUserById(string userId);
    }
}
