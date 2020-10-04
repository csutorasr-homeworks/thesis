using AutoMapper;
using Flottapp.Domain;
using Flottapp.Infrastucture;
using System.Linq;

namespace Flottapp.Application.Registration
{
    class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<Domain.Registration, RegistrationVm>()
                .ForMember(x => x.Time, x => x.MapFrom(r => r.CreationTime));
        }
    }
}
