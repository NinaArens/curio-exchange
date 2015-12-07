using CurioExchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurioExchange.Interfaces
{
    public interface IUserAgent
    {
        Task<ICollection<AspNetUserModel>> RetrieveUsers();

        Task<AspNetUserModel> RetrieveUserById(string userId);
    }
}
