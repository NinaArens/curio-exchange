using CurioExchange.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurioExchange.Models;
using CurioExchangeService;
using AutoMapper;
using CurioExchangeService.Entities;

namespace CurioExchange.Agents
{
    public class PieceAgent : IPieceAgent
    {
        private IPieceService _pieceService;

        public PieceAgent(IPieceService pieceService)
        {
            _pieceService = pieceService;
        }

        public async Task<ICollection<PieceModel>> RetrievePieces()
        {
            var result = await _pieceService.RetrievePieces();
            return Mapper.Map<ICollection<Piece>, ICollection<PieceModel>>(result);
        }

        public async Task<ICollection<UserPieceModel>> RetrieveUserPieces(string userId)
        {
            var result = await _pieceService.RetrieveUserPieces(userId);
            return Mapper.Map<ICollection<UserPiece>, ICollection<UserPieceModel>>(result);
        }

        public async Task<int> CreaseUserPiece(UserPieceModel userPiece)
        {
            var mapped = Mapper.Map<UserPieceModel, UserPiece>(userPiece);
            return await _pieceService.CreaseUserPiece(mapped);
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
                    result.Add(await _pieceService.CreaseUserPiece(mapped));
                }
            }
            return result;
        }

        public async Task DeleteUserPiece(int id)
        {
            await _pieceService.DeleteUserPiece(id);
        }

        public async Task<ICollection<UserPieceModel>> RetrieveTradesWanted(string userId)
        {
            var result = await _pieceService.RetrieveTradesWanted(userId);
            return Mapper.Map<ICollection<UserPiece>, ICollection<UserPieceModel>>(result);
        }

        public async Task<ICollection<UserPieceModel>> RetrieveTradesOwned(string userId)
        {
            var result = await _pieceService.RetrieveTradesOwned(userId);
            return Mapper.Map<ICollection<UserPiece>, ICollection<UserPieceModel>>(result);
        }

        public async Task<int> GetPieceIdForName(string name)
        {
            return await _pieceService.GetPieceIdForName(name);
        }
    }
}