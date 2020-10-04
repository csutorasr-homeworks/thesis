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
    [Route("api/Fleets")]
    public class CarsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CarsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("{id:length(24)}/cars")]
        public async Task<IEnumerable<CarRowVm>> GetCars(string id, CancellationToken cancellationToken)
        {
            return await mediator.Send(new ListCarsForFleetQuery { FleetId = id }, cancellationToken);
        }
        [HttpGet("{id:length(24)}/cars/{carId:length(24)}")]
        public async Task<CarVm> GetCar(string id, string carId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCarForFleetQuery { FleetId = id, CarId = carId }, cancellationToken);
        }
        [HttpPost("{id:length(24)}/cars")]
        public async Task<string> PostCar(string id, CreateCarForFleetCommand.Dto dto, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreateCarForFleetCommand { FleetId = id, Data = dto }, cancellationToken);
        }
        [HttpPut("{id:length(24)}/cars/{carId:length(24)}")]
        public async Task PutCar(string id, string carId, ModifyCarForFleetCommand.Dto dto, CancellationToken cancellationToken)
        {
            await mediator.Send(new ModifyCarForFleetCommand { FleetId = id, CarId = carId, Data = dto }, cancellationToken);
        }
        [HttpDelete("{id:length(24)}/cars/{carId:length(24)}")]
        public async Task DeactivateCar(string id, string carId, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeactivateCarForFleetCommand { FleetId = id, CarId = carId }, cancellationToken);
        }
    }
}
