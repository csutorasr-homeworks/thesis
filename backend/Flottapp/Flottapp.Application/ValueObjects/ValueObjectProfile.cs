using AutoMapper;
using Flottapp.Infrastucture;

namespace Flottapp.Application
{
    class ValueObjectProfile : Profile
    {
        public ValueObjectProfile()
        {
            CreateMap<Domain.Money, MoneyVm>().ReverseMap();
            CreateMap<Domain.Location, LocationVm>().ReverseMap();
        }
    }
}
