using Flottapp.Application.ServiceRules;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateMileageServiceRuleForCar : IRequest<string>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public Dto Data { get; set; }
        public class Handler : IRequestHandler<CreateMileageServiceRuleForCar, string>
        {
            private readonly IServiceRulesStore serviceRulesStore;

            public Handler(IServiceRulesStore serviceRulesStore)
            {
                this.serviceRulesStore = serviceRulesStore;
            }
            public async Task<string> Handle(CreateMileageServiceRuleForCar request, CancellationToken cancellationToken)
            {
                var id = await serviceRulesStore.CreateMileageServiceRuleForCar(request.FleetId, request.CarId, request.Data.TravelledMileage, cancellationToken);
                return id;
            }
        }
        public class Dto
        {
            public decimal TravelledMileage { get; set; }
        }
    }
}
