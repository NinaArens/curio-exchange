using CurioExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurioExchange.Interfaces
{
    public interface IUserAgent
    {
        Task<ICollection<AspNetUserModel>> RetrieveUsers();

        Task<AspNetUserModel> RetrieveUserById(string userId);
    }
}
