using AutoMapper;
using Flottapp.Infrastucture;
using Flottapp.Infrastucture.ViewModels;

namespace Flottapp.Application.ServiceRules
{
    class ServiceRulesProfile : Profile
    {
        public ServiceRulesProfile()
        {
            CreateMap<Domain.ServiceRule, ServiceRuleVm>()
                .IncludeAllDerived();
            CreateMap<Domain.MileageServiceRule, MileageServiceRuleVm>();
            CreateMap<Domain.TimeServiceRule, TimeServiceRuleVm>();
        }
    }
}
