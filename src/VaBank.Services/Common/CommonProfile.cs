﻿using AutoMapper;
using VaBank.Core.Membership;
using VaBank.Core.Membership.Entities;
using VaBank.Services.Contracts.Common.Models;

namespace VaBank.Services.Common
{
    public class CommonProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<User, UserNameModel>()
                .ForMember(x => x.Email, cfg => cfg.MapFrom(y => y.Profile == null ? null : y.Profile.Email))
                .ForMember(x => x.FirstName, cfg => cfg.MapFrom(y => y.Profile == null ? null : y.Profile.FirstName))
                .ForMember(x => x.LastName, cfg => cfg.MapFrom(y => y.Profile == null ? null : y.Profile.LastName));
        }
    }
}
