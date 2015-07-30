using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CurioExchangeService.Entities;
using CurioExchange.Models;
using CurioExchangeService.Entities;

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

            Mapper.CreateMap<UserPiece, UserPieceModel>();
            Mapper.CreateMap<UserPieceModel, UserPiece>();

            Mapper.CreateMap<UserSet, UserSetModel>();
            Mapper.CreateMap<UserSetModel, UserSet>();

            Mapper.CreateMap<AspNetUser, AspNetUserModel>();
            Mapper.CreateMap<AspNetUserModel, AspNetUser>();
        }
    }
}