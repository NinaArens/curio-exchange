using CurioExchange.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurioExchange.Models;
using CurioExchangeService;
using AutoMapper;
using CurioExchangeService.Entities;

namespace CurioExchange.Agents
{
    public class UserSetAgent : IUserSetAgent
    {
        private IUserSetService _userSetService;

        public UserSetAgent(IUserSetService userSetService)
        {
            _userSetService = userSetService;
        }

        public async Task<ICollection<UserSetModel>> RetrieveUserSets(string userId, string name = null)
        {
            var result = await _userSetService.RetrieveUserSets(userId);
            var mapped = Mapper.Map<ICollection<UserSet>, ICollection<UserSetModel>>(result);

            if (name != null && name != "")
            { 
                foreach (var item in mapped)
                {
                    if (item.Owned)
                        item.ListedByMe = await _userSetService.WantedBy(item.Set_Id, name);
                    else
                        item.ListedByMe = await _userSetService.OwnedBy(item.Set_Id, name);
                }
            }

            return mapped;
        }

        public async Task<ICollection<UserSetModel>> RetrieveUserSetsWanted(string userId)
        {
            var result = await _userSetService.RetrieveUserSetsWanted(userId);
            return Mapper.Map<ICollection<UserSet>, ICollection<UserSetModel>>(result);
        }

        public async Task<ICollection<UserSetModel>> RetrieveUserSetsOwned(string userId)
        {
            var result = await _userSetService.RetrieveUserSetsOwned(userId);
            return Mapper.Map<ICollection<UserSet>, ICollection<UserSetModel>>(result);
        }

        public async Task<int> CreaseUserSet(UserSetModel userSet)
        {
            var mapped = Mapper.Map<UserSetModel, UserSet>(userSet);
            return await _userSetService.CreaseUserSet(mapped);
        }

        public async Task<IEnumerable<int>> CreaseUserSets(UserSetModel userSet)
        {
            var result = new List<int>();
            foreach (var item in userSet.Set_Ids)
            {
                userSet.Set_Id = item;
                var mapped = Mapper.Map<UserSetModel, UserSet>(userSet);

                for (int i = 0; i < userSet.Amount; i++)
                {
                    result.Add(await _userSetService.CreaseUserSet(mapped));
                }
            }
            return result;
        }

        public async Task DeleteUserSet(int id)
        {
            await _userSetService.DeleteUserSet(id);
        }

        public async Task DeleteUserSets(string userId, bool owned)
        {
            await _userSetService.DeleteUserSets(userId, owned);
        }

        public async Task<ICollection<UserSetModel>> RetrieveTradesWanted(string userId)
        {
            var result = await _userSetService.RetrieveTradesWanted(userId);
            return Mapper.Map<ICollection<UserSet>, ICollection<UserSetModel>>(result);
        }

        public async Task<ICollection<UserSetModel>> RetrieveTradesOwned(string userId)
        {
            var result = await _userSetService.RetrieveTradesOwned(userId);
            return Mapper.Map<ICollection<UserSet>, ICollection<UserSetModel>>(result);
        }

        public async Task RefreshUserSet(int id)
        {
            await _userSetService.RefreshUserSet(id);
        }
    }
}