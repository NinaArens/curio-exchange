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
    public class PieceService : IPieceService
    {
        private CurioExchangeContext _context;

        public PieceService(CurioExchangeContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Piece>> RetrievePieces()
        {
            return await _context.Pieces.Include(t => t.Set.Collection).ToListAsync();
        }

        public async Task<ICollection<UserPiece>> RetrieveUserPieces(string userId)
        {
            return await _context.UserPieces.Where(t => t.User.Id == userId).Include(t => t.Piece.Set.Collection).ToListAsync();
        }

        public async Task<int> CreaseUserPiece(UserPiece userPiece)
        {
            try
            {
                _context.UserPieces.Add(userPiece);
                await _context.SaveChangesAsync();
                return userPiece.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task DeleteUserPiece(int id)
        {
            var userPiece = await _context.UserPieces.FindAsync(id);
            _context.UserPieces.Remove(userPiece);
            await _context.SaveChangesAsync();
        }
    }
}
