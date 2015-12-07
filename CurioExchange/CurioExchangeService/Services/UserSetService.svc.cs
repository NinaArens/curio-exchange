using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurioExchangeService.Entities;
using System.Data.Entity;

namespace CurioExchangeService
{
    public class UserSetService : IUserSetService
    {
        private CurioExchangeContext _context;

        public UserSetService(CurioExchangeContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserSet>> RetrieveUserSets(string userId)
        {
            return await _context.UserSets.Where(t => t.User.Id == userId).Include(t => t.Set.Collection).OrderBy(t => t.Set.Collection.Name).ThenBy(t => t.Set.Name).ToListAsync();
        }

        public async Task<ICollection<UserSet>> RetrieveUserSetsWanted(string userId)
        {
            return await _context.UserSets.Where(t => t.User.Id == userId && !t.Owned).Include(t => t.Set.Collection).OrderBy(t => t.Set.Collection.Name).ThenBy(t => t.Set.Name).ToListAsync();
        }

        public async Task<ICollection<UserSet>> RetrieveUserSetsOwned(string userId)
        {
            return await _context.UserSets.Where(t => t.User.Id == userId && t.Owned).Include(t => t.Set.Collection).OrderBy(t => t.Set.Collection.Name).ThenBy(t => t.Set.Name).ToListAsync();
        }

        public async Task<int> CreaseUserSet(UserSet userSet)
        {
            _context.UserSets.Add(userSet);
            await _context.SaveChangesAsync();
            return userSet.Id;
        }

        public async Task DeleteUserSet(int id)
        {
            var userSet = await _context.UserSets.FindAsync(id);
            _context.UserSets.Remove(userSet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserSets(string userId, bool owned)
        {
            var toDelete = await _context.UserSets.Where(t => t.User.Id == userId && t.Owned == owned).ToListAsync();

            if (toDelete != null && toDelete.Count > 0)
            {
                _context.UserSets.RemoveRange(toDelete);
            }
        }

        public async Task<ICollection<UserSet>> RetrieveTradesWanted(string userId)
        {
            var wantedIds = _context.UserSets.Where(t => t.User_Id == userId && t.Owned == false).Select(t => t.Set_Id);
            return await _context.UserSets.Where(t => t.Owned == true && wantedIds.Contains(t.Set_Id) && t.User_Id != userId).OrderBy(t => t.Set.Collection.Name).ThenBy(t => t.Set.Name).ToListAsync();
        }

        public async Task<ICollection<UserSet>> RetrieveTradesOwned(string userId)
        {
            var ownedIds = _context.UserSets.Where(t => t.User_Id == userId && t.Owned).Select(t => t.Set_Id);
            return await _context.UserSets.Where(t => t.Owned == false && ownedIds.Contains(t.Set_Id) && t.User_Id != userId).OrderBy(t => t.Set.Collection.Name).ThenBy(t => t.Set.Name).ToListAsync();
        }

        public async Task RefreshUserSet(int id)
        {
            var userSet = await _context.UserSets.FindAsync(id);
            userSet.Added = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> WantedBy(int SetId, string name)
        {
            var wanted = await _context.UserSets.FirstOrDefaultAsync(t => t.User.UserName == name && t.Set_Id == SetId && t.Owned == false);
            return wanted != null;
        }

        public async Task<bool> OwnedBy(int SetId, string name)
        {
            var owned = await _context.UserSets.FirstOrDefaultAsync(t => t.User.UserName == name && t.Set_Id == SetId && t.Owned == true);
            return owned != null;
        }
    }
}
