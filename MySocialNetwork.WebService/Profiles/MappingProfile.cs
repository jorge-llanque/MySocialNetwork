using AutoMapper;
using MySocialNetwork.Dto;
using MySocialNetwork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySocialNetwork.WebService.Profiles
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            /* Basic DTOs mapping*/
            CreateMap<ProfileDto, MySocialNetwork.Model.Entities.Profile>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
