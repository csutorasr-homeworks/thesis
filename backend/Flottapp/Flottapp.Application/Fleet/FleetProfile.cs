using AutoMapper;
using Flottapp.Infrastucture;

namespace Flottapp.Application.Fleet
{
    class FleetProfile : Profile
    {
        public FleetProfile()
        {
            CreateMap<Domain.Fleet, FleetRowVm>();
        }
    }
}
