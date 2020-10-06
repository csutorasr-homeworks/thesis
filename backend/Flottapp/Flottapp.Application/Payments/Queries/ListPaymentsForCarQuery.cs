using AutoMapper;
using Flottapp.Application.Payments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class ListPaymentsForCarQuery : IRequest<IEnumerable<PaymentRowVm>>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public class Handler : IRequestHandler<ListPaymentsForCarQuery, IEnumerable<PaymentRowVm>>
        {
            private readonly IPaymentsStore paymentsStore;
            private readonly IMapper mapper;

            public Handler(IPaymentsStore paymentsStore, IMapper mapper)
            {
                this.paymentsStore = paymentsStore;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<PaymentRowVm>> Handle(ListPaymentsForCarQuery request, CancellationToken cancellationToken)
            {
                var data = await paymentsStore.GetPaymentsForCar(request.FleetId, request.CarId, cancellationToken);
                return mapper.Map<IEnumerable<PaymentRowVm>>(data);
            }
        }
    }
}
