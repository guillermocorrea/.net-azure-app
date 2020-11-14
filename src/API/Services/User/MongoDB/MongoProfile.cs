using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace API.Services.User.MongoDB
{
    public class MongoProfile : Profile
    {
        public MongoProfile()
        {
            CreateMap<Model.User, UserDb>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));
            CreateMap<UserDb, Model.User>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));
        }
    }
}