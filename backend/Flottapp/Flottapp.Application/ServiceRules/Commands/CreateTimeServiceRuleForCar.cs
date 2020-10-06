using Flottapp.Application.ServiceRules;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateTimeServiceRuleForCar : IRequest<string>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public Dto Data { get; set; }
        public class Handler : IRequestHandler<CreateTimeServiceRuleForCar, string>
        {
            private readonly IServiceRulesStore serviceRulesStore;

            public Handler(IServiceRulesStore serviceRulesStore)
            {
                this.serviceRulesStore = serviceRulesStore;
            }
            public async Task<string> Handle(CreateTimeServiceRuleForCar request, CancellationToken cancellationToken)
            {
                var id = await serviceRulesStore.CreateTimeServiceRuleForCar(request.FleetId, request.CarId, request.Data.IntervalInMonth, cancellationToken);
                return id;
            }
        }
        public class Dto
        {
            public int IntervalInMonth { get; set; }
        }
    }
}
