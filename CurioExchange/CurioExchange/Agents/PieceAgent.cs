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

        public async Task DeleteUserPiece(UserPieceModel userPiece)
        {
            var mapped = Mapper.Map<UserPieceModel, UserPiece>(userPiece);
            await _pieceService.DeleteUserPiece(mapped);
        }
    }
}