using AutoMapper;
using Flottapp.Domain;
using Flottapp.Infrastucture;
using System.Linq;

namespace Flottapp.Application.MonthlyAggregate
{
    class MonthlyAggregatesProfile : Profile
    {
        public MonthlyAggregatesProfile()
        {
            CreateMap<Domain.MontlyAggregate, MonthlyAggregateRowVm>();
        }
    }
}
