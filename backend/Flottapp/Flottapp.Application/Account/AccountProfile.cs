using AutoMapper;
using Flottapp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Application.Account
{
    class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<UserProfile, ProfileVm>();
        }
    }
}
