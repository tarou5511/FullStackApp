using System;
using FullStackApp.Api.Models.Entities;
using AutoMapper;
using FullStackApp.Api.Models.DTOs;

namespace FullStackApp.Api.Config;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}
