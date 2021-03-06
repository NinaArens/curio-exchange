﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurioExchangeService.Entities;
using System.Data.Entity;

namespace CurioExchangeService
{
    public class UserPieceService : IUserPieceService
    {
        private CurioExchangeContext _context;

        public UserPieceService(CurioExchangeContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserPiece>> RetrieveUserPieces(string userId)
        {
            return await _context.UserPieces.Where(t => t.User.Id == userId).Include(t => t.Piece.Set.Collection).OrderBy(t => t.Piece.Set.Collection.Name).ThenBy(t => t.Piece.Set.Name).ToListAsync();
        }

        public async Task<ICollection<UserPiece>> RetrieveUserPiecesWanted(string userId)
        {
            return await _context.UserPieces.Where(t => t.User.Id == userId && !t.Owned).Include(t => t.Piece.Set.Collection).OrderBy(t => t.Piece.Set.Collection.Name).ThenBy(t => t.Piece.Set.Name).ToListAsync();
        }

        public async Task<ICollection<UserPiece>> RetrieveUserPiecesOwned(string userId)
        {
            return await _context.UserPieces.Where(t => t.User.Id == userId && t.Owned).Include(t => t.Piece.Set.Collection).OrderBy(t => t.Piece.Set.Collection.Name).ThenBy(t => t.Piece.Set.Name).ToListAsync();
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

        public async Task RefreshUserPiece(int id)
        {
            var userPiece = await _context.UserPieces.FindAsync(id);
            userPiece.Added = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> WantedBy(int pieceId, string name)
        {
            var wanted = await _context.UserPieces.FirstOrDefaultAsync(t => t.User.UserName == name && t.Piece_Id == pieceId && t.Owned == false);
            return wanted != null;
        }

        public async Task<bool> OwnedBy(int pieceId, string name)
        {
            var owned = await _context.UserPieces.FirstOrDefaultAsync(t => t.User.UserName == name && t.Piece_Id == pieceId && t.Owned == true);
            return owned != null;
        }
    }
}
