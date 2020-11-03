using AutoMapper;
using Flottapp.Infrastucture;

namespace Flottapp.Application.Fleet
{
    class FleetProfile : Profile
    {
        public FleetProfile()
        {
            CreateMap<Domain.Fleet, FleetRowVm>();
            CreateMap<Domain.Fleet, FleetVm>()
                .ForMember(x => x.Users, x => x.Ignore());
        }
    }
}
