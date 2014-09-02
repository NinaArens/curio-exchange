using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CurioExchange.Entities;
using CurioExchange.Models;

namespace CurioExchange.Plumbing
{
    public class MapperConfiguration
    {
        public static void Map()
        {
            Mapper.CreateMap<Piece, PieceModel>();
            Mapper.CreateMap<PieceModel, Piece>();

            Mapper.CreateMap<Set, SetModel>();
            Mapper.CreateMap<SetModel, Set>();

            Mapper.CreateMap<Collection, CollectionModel>();
            Mapper.CreateMap<CollectionModel, Collection>();
        }
    }
}