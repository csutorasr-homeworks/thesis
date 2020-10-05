using Flottapp.Infrastucture;
using Flottapp.Infrastucture.Commands;
using Flottapp.Infrastucture.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Controllers
{
    [ApiController]
    [Route("api/fleets/{fleetId:length(24)}/cars/{carId:length(24)}/monthly-aggregate")]
    public class MonthlyAggregateController : ControllerBase
    {
        private readonly IMediator mediator;

        public MonthlyAggregateController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<MonthlyAggregateRowVm>> GetCar(string fleetId, string carId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new ListMonthlyAggregatesForCarQuery { FleetId = fleetId, CarId = carId }, cancellationToken);
        }
        [HttpPost("{maId:length(24)}/accept")]
        public async Task PostCar(string fleetId, string carId, string maId, CancellationToken cancellationToken)
        {
            await mediator.Send(new AcceptMontlyAggregatefForCarCommand { FleetId = fleetId, CarId = carId, AggregateId = maId }, cancellationToken);
        }
        [HttpDelete("{maId:length(24)}/reject")]
        public async Task DeactivateCar(string fleetId, string carId, string maId, CancellationToken cancellationToken)
        {
            await mediator.Send(new RejectMontlyAggregatefForCarCommand { FleetId = fleetId, CarId = carId, AggregateId = maId }, cancellationToken);
        }
    }
}
