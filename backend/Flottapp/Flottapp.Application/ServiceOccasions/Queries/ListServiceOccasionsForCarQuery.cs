using AutoMapper;
using Flottapp.Application.ServiceOccasions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class ListServiceOccasionsForCarQuery : IRequest<IEnumerable<ServiceOccasionVm>>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public int PageSize { get; set; }
        public int PageLength { get; set; }
        public class Handler : IRequestHandler<ListServiceOccasionsForCarQuery, IEnumerable<ServiceOccasionVm>>
        {
            private readonly IServiceOccasionsStore serviceOccasionsStore;
            private readonly IMapper mapper;

            public Handler(IServiceOccasionsStore serviceOccasionsStore, IMapper mapper)
            {
                this.serviceOccasionsStore = serviceOccasionsStore;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<ServiceOccasionVm>> Handle(ListServiceOccasionsForCarQuery request, CancellationToken cancellationToken)
            {
                var data = await serviceOccasionsStore.GetServiceOccasionForCar(request.FleetId, request.CarId, cancellationToken);
                return mapper.Map<IEnumerable<ServiceOccasionVm>>(data);
            }
        }
    }
}
