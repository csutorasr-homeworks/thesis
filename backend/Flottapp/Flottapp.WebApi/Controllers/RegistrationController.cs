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
    [Route("api/fleets/{id:length(24)}/cars/{carId:length(24)}/registrations")]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator mediator;

        public RegistrationController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<RegistrationVm>> GetCar(string id, string carId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new ListRegistrationsForCarQuery { FleetId = id, CarId = carId }, cancellationToken);
        }
        [HttpPost]
        public async Task<string> PostCar(string id, string carId, CreateRegistrationForCarCommand.Dto dto, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreateRegistrationForCarCommand { FleetId = id, CarId = carId, Data = dto }, cancellationToken);
        }
        [HttpDelete("{regId:length(24)}")]
        public async Task DeactivateCar(string id, string carId, string regId, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteRegistrationForCarCommand { FleetId = id, CarId = carId, RegistrationId = regId }, cancellationToken);
        }
    }
}
