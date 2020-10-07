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
    [Route("api/fleets/{id:length(24)}/cars/{carId:length(24)}/service-occasions")]
    public class ServiceOccasionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ServiceOccasionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<ServiceOccasionVm>> GetCar(string id, string carId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new ListServiceOccasionsForCarQuery { FleetId = id, CarId = carId }, cancellationToken);
        }
        [HttpPost]
        public async Task<string> PostCar(string id, string carId, CreateServiceOccasionForCarCommand.Dto dto, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreateServiceOccasionForCarCommand { FleetId = id, CarId = carId, Data = dto }, cancellationToken);
        }
        [HttpDelete("{occasionId:length(24)}")]
        public async Task DeactivateCar(string id, string carId, string occasionId, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteServiceOccasionForCarCommand { FleetId = id, CarId = carId, OccasionId = occasionId }, cancellationToken);
        }
    }
}
