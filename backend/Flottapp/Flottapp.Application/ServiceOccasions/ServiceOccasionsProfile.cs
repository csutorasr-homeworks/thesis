using AutoMapper;
using Flottapp.Infrastucture;

namespace Flottapp.Application.ServiceOccasions
{
    class ServiceOccasionsProfile : Profile
    {
        public ServiceOccasionsProfile()
        {
            CreateMap<Domain.ServiceOccasion, ServiceOccasionVm>()
                .ForMember(x => x.DateTime, y => y.MapFrom(x => x.CreationTime));
        }
    }
}
