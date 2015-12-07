using CurioExchangeService.Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CurioExchangeService
{
    [ServiceContract]
    public interface ISetService
    {
        [OperationContract]
        Task<ICollection<Set>> RetrieveSets();

        [OperationContract]
        Task<int> GetSetIdForName(string set);
    }
}
