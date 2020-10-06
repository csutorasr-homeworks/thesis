using Flottapp.Application.ServiceRules;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class DeleteServiceRuleForCar : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public string SerivceRuleId { get; set; }
        public class Handler : IRequestHandler<DeleteServiceRuleForCar>
        {
            private readonly IServiceRulesStore serviceRulesStore;

            public Handler(IServiceRulesStore serviceRulesStore)
            {
                this.serviceRulesStore = serviceRulesStore;
            }
            public async Task<Unit> Handle(DeleteServiceRuleForCar request, CancellationToken cancellationToken)
            {
                await serviceRulesStore.DeleteServiceRuleForCar(request.FleetId, request.CarId, request.SerivceRuleId, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
