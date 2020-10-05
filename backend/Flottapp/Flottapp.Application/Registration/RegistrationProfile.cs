using AutoMapper;
using Flottapp.Infrastucture;
using Flottapp.Infrastucture.Commands;

namespace Flottapp.Application.Registration
{
    class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<Domain.Registration, RegistrationVm>()
                .ForMember(x => x.Time, x => x.MapFrom(r => r.CreationTime));
            CreateMap<Domain.Registration, CreateRegistrationForCarEvent>().ConstructUsing(registration => new CreateRegistrationForCarEvent
            {
                Registration = registration,
            });
            CreateMap<DeleteRegistrationForCarCommand, DeleteRegistrationForCarEvent>();
        }
    }
}
