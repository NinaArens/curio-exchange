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
            _context.UserPieces.Add(userPiece);
            await _context.SaveChangesAsync();
            return userPiece.Id;
        }

        public async Task DeleteUserPiece(int id)
        {
            var userPiece = await _context.UserPieces.FindAsync(id);
            _context.UserPieces.Remove(userPiece);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserPiece>> RetrieveTradesWanted(string userId)
        {
            var wantedIds = _context.UserPieces.Where(t => t.User_Id == userId && t.Owned == false).Select(t => t.Piece_Id);
            return await _context.UserPieces.Where(t => t.Owned == true && wantedIds.Contains(t.Piece_Id) && t.User_Id != userId).ToListAsync();
        }

        public async Task<ICollection<UserPiece>> RetrieveTradesOwned(string userId)
        {
            var ownedIds = _context.UserPieces.Where(t => t.User_Id == userId && t.Owned).Select(t => t.Piece_Id);
            return await _context.UserPieces.Where(t => t.Owned == false && ownedIds.Contains(t.Piece_Id) && t.User_Id != userId).ToListAsync();
        }
    }
}
