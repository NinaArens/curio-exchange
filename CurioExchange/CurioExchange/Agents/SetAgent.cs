using CurioExchange.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurioExchange.Models;
using CurioExchangeService;
using AutoMapper;
using CurioExchangeService.Entities;

namespace CurioExchange.Agents
{
    public class SetAgent : ISetAgent
    {
        private ISetService _SetService;

        public SetAgent(ISetService SetService)
        {
            _SetService = SetService;
        }

        public async Task<ICollection<SetModel>> RetrieveSets()
        {
            var result = await _SetService.RetrieveSets();
            return Mapper.Map<ICollection<Set>, ICollection<SetModel>>(result);
        }

        public async Task<int> GetSetIdForName(string set)
        {
            return await _SetService.GetSetIdForName(set);
        }
    }
}