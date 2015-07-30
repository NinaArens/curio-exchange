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
    public interface ISetService
    {
        [OperationContract]
        Task<ICollection<Set>> RetrieveSets();

        [OperationContract]
        Task<int> GetSetIdForName(string set);
    }
}
