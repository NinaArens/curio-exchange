using CurioExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurioExchange.Interfaces
{
    public interface ISetAgent
    {
        Task<ICollection<SetModel>> RetrieveSets();

        Task<int> GetSetIdForName(string set);
    }
}
