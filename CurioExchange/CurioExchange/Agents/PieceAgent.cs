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

        public async Task<int> GetPieceIdForName(string set, string piece)
        {
            return await _pieceService.GetPieceIdForName(set, piece);
        }
    }
}