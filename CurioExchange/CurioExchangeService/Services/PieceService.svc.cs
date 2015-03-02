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
            return await _context.Pieces.Include(t => t.Set.Collection).OrderBy(t => t.Set.Collection.Name).ThenBy(t => t.Set.Name).ToListAsync();
        }

        public async Task<ICollection<UserPiece>> RetrieveUserPieces(string userId)
        {
            return await _context.UserPieces.Where(t => t.User.Id == userId).Include(t => t.Piece.Set.Collection).OrderBy(t => t.Piece.Set.Collection.Name).ThenBy(t => t.Piece.Set.Name).ToListAsync();
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

        public async Task DeleteUserPieces(string userId, bool owned)
        {
            var toDelete = await _context.UserPieces.Where(t => t.User.Id == userId && t.Owned == owned).ToListAsync();

            if (toDelete != null && toDelete.Count > 0)
            {
                _context.UserPieces.RemoveRange(toDelete);
            }  
        }

        public async Task<ICollection<UserPiece>> RetrieveTradesWanted(string userId)
        {
            var wantedIds = _context.UserPieces.Where(t => t.User_Id == userId && t.Owned == false).Select(t => t.Piece_Id);
            return await _context.UserPieces.Where(t => t.Owned == true && wantedIds.Contains(t.Piece_Id) && t.User_Id != userId).OrderBy(t => t.Piece.Set.Collection.Name).ThenBy(t => t.Piece.Set.Name).ToListAsync();
        }

        public async Task<ICollection<UserPiece>> RetrieveTradesOwned(string userId)
        {
            var ownedIds = _context.UserPieces.Where(t => t.User_Id == userId && t.Owned).Select(t => t.Piece_Id);
            return await _context.UserPieces.Where(t => t.Owned == false && ownedIds.Contains(t.Piece_Id) && t.User_Id != userId).OrderBy(t => t.Piece.Set.Collection.Name).ThenBy(t => t.Piece.Set.Name).ToListAsync();
        }

        public async Task<int> GetPieceIdForName(string set, string piece)
        {
            var result = await _context.Pieces.FirstOrDefaultAsync(t => t.Name.ToLower().Contains(piece.ToLower()) && t.Name.ToLower().StartsWith(set.ToLower()) && t.Set.Name.ToLower().StartsWith(set.ToLower()));
            if (result == null)
                return 0;
            return result.Id;
        }

        public async Task RefreshUserPiece(int id)
        {
            var userPiece = await _context.UserPieces.FindAsync(id);
            userPiece.Added = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
