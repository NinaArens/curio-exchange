using CurioExchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurioExchange.Interfaces
{
    public interface ISetAgent
    {
        Task<ICollection<SetModel>> RetrieveSets();

        Task<int> GetSetIdForName(string set);
    }
}
