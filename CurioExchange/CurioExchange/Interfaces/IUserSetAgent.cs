using CurioExchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurioExchange.Interfaces
{
    public interface IUserSetAgent
    {
        Task<ICollection<UserSetModel>> RetrieveUserSets(string userId, string name = null);

        Task<ICollection<UserSetModel>> RetrieveUserSetsWanted(string userId);

        Task<ICollection<UserSetModel>> RetrieveUserSetsOwned(string userId);

        Task<int> CreaseUserSet(UserSetModel userSet);

        Task<IEnumerable<int>> CreaseUserSets(UserSetModel userSet);

        Task DeleteUserSet(int id);

        Task DeleteUserSets(string userId, bool owned);

        Task<ICollection<UserSetModel>> RetrieveTradesWanted(string userId);

        Task<ICollection<UserSetModel>> RetrieveTradesOwned(string userId);

        Task RefreshUserSet(int id);
    }
}
