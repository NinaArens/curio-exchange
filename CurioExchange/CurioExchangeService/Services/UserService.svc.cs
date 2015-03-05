using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CurioExchangeService.Entities;
using System.Data.Entity;

namespace CurioExchangeService
{
    public class UserService : IUserService
    {
        private CurioExchangeContext _context;

        public UserService(CurioExchangeContext context)
        {
            _context = context;
        }

        public async Task<ICollection<AspNetUser>> RetrieveUsers()
        {
            return await _context.AspNetUsers.ToListAsync();
        }

        public async Task<AspNetUser> RetrieveUserById(string userId)
        {
            return await _context.AspNetUsers.FirstOrDefaultAsync(t => t.Id == userId);
        }
    }
}
