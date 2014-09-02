using CurioExchange.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using CurioExchange.Models;
using CurioExchangeService;
using AutoMapper;
using CurioExchange.Entities;

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
    }
}