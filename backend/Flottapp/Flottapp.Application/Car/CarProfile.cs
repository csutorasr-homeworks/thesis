﻿using AutoMapper;
using Flottapp.Domain;
using Flottapp.Infrastucture;
using System.Linq;

namespace Flottapp.Application.Car
{
    class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Domain.Car, CarRowVm>()
                .ForMember(x => x.NeedsToBeServiced, opts => opts.MapFrom<CarNeedsToBeServicedResolver>());
        }
    }

    class CarNeedsToBeServicedResolver : IValueResolver<Domain.Car, CarRowVm, bool>
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public CarNeedsToBeServicedResolver(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }
        public bool Resolve(Domain.Car car, CarRowVm destination, bool destMember, ResolutionContext context)
        {
            return car.ServiceRules?.Any(x => x.NeedsService(car, dateTimeProvider)) ?? false;
        }
    }
}
