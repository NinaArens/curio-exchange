using CurioExchange.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurioExchange.Models;
using CurioExchangeService;
using AutoMapper;
using CurioExchangeService.Entities;

namespace CurioExchange.Agents
{
    public class UserPieceAgent : IUserPieceAgent
    {
        private IUserPieceService _userPieceService;

        public UserPieceAgent(IUserPieceService userPieceService)
        {
            _userPieceService = userPieceService;
        }

        public async Task<ICollection<UserPieceModel>> RetrieveUserPieces(string userId, string name = null)
        {
            var result = await _userPieceService.RetrieveUserPieces(userId);
            var mapped = Mapper.Map<ICollection<UserPiece>, ICollection<UserPieceModel>>(result);

            if (name != null && name != "")
            { 
                foreach (var item in mapped)
                {
                    if (item.Owned)
                        item.ListedByMe = await _userPieceService.WantedBy(item.Piece_Id, name);
                    else
                        item.ListedByMe = await _userPieceService.OwnedBy(item.Piece_Id, name);
                }
            }

            return mapped;
        }

        public async Task<int> CreaseUserPiece(UserPieceModel userPiece)
        {
            var mapped = Mapper.Map<UserPieceModel, UserPiece>(userPiece);
            return await _userPieceService.CreaseUserPiece(mapped);
        }

        public async Task<IEnumerable<int>> CreaseUserPieces(UserPieceModel userPiece)
        {
            var result = new List<int>();
            foreach (var item in userPiece.Piece_Ids)
            {
                userPiece.Piece_Id = item;
                var mapped = Mapper.Map<UserPieceModel, UserPiece>(userPiece);

                for (int i = 0; i < userPiece.Amount; i++)
                {
                    result.Add(await _userPieceService.CreaseUserPiece(mapped));
                }
            }
            return result;
        }

        public async Task DeleteUserPiece(int id)
        {
            await _userPieceService.DeleteUserPiece(id);
        }

        public async Task DeleteUserPieces(string userId, bool owned)
        {
            await _userPieceService.DeleteUserPieces(userId, owned);
        }

        public async Task<ICollection<UserPieceModel>> RetrieveTradesWanted(string userId)
        {
            var result = await _userPieceService.RetrieveTradesWanted(userId);
            return Mapper.Map<ICollection<UserPiece>, ICollection<UserPieceModel>>(result);
        }

        public async Task<ICollection<UserPieceModel>> RetrieveTradesOwned(string userId)
        {
            var result = await _userPieceService.RetrieveTradesOwned(userId);
            return Mapper.Map<ICollection<UserPiece>, ICollection<UserPieceModel>>(result);
        }

        public async Task RefreshUserPiece(int id)
        {
            await _userPieceService.RefreshUserPiece(id);
        }
    }
}