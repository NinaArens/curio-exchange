using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurioExchangeService.Entities;
using System.Data.Entity;

namespace CurioExchangeService
{
    public class PieceService : IPieceService
    {
        private CurioExchangeContext _context;

        public PieceService(CurioExchangeContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Piece>> RetrievePieces()
        {
            return await _context.Pieces.Include(t => t.Set.Collection).OrderBy(t => t.Set.Collection.Name).ThenBy(t => t.Set.Name).ToListAsync();
        }

        public async Task<int> GetPieceIdForName(string set, string piece)
        {
            var result = await _context.Pieces.FirstOrDefaultAsync(t => t.Name.ToLower().Contains(piece.ToLower()) && t.Name.ToLower().StartsWith(set.ToLower()) && t.Set.Name.ToLower().StartsWith(set.ToLower()));
            if (result == null)
                return 0;
            return result.Id;
        }
    }
}
