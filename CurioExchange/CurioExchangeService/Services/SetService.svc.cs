using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurioExchangeService.Entities;
using System.Data.Entity;

namespace CurioExchangeService
{
    public class SetService : ISetService
    {
        private CurioExchangeContext _context;

        public SetService(CurioExchangeContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Set>> RetrieveSets()
        {
            return await _context.Sets.Include(t => t.Collection).OrderBy(t => t.Collection.Name).ThenBy(t => t.Name).ToListAsync();
        }

        public async Task<int> GetSetIdForName(string set)
        {
            var result = await _context.Sets.FirstOrDefaultAsync(t => t.Name.ToLower().StartsWith(set.ToLower()));
            if (result == null)
                return 0;
            return result.Id;
        }
    }
}
