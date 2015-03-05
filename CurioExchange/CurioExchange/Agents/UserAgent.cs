using CurioExchange.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurioExchange.Models;
using CurioExchangeService;
using AutoMapper;
using CurioExchangeService.Entities;

namespace CurioExchange.Agents
{
    public class UserAgent : IUserAgent
    {
        private IUserService _userService;

        public UserAgent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ICollection<AspNetUserModel>> RetrieveUsers()
        {
            var result = await _userService.RetrieveUsers();
            return Mapper.Map<ICollection<AspNetUser>, ICollection<AspNetUserModel>>(result);
        }

        public async Task<AspNetUserModel> RetrieveUserById(string userId)
        {
            var result = await _userService.RetrieveUserById(userId);
            return Mapper.Map<AspNetUser, AspNetUserModel>(result);
        }
    }
}