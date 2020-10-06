using Flottapp.Infrastucture;
using Flottapp.Infrastucture.Commands;
using Flottapp.Infrastucture.Queries;
using Flottapp.Infrastucture.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Controllers
{
    [ApiController]
    [Route("api/fleets/{id:length(24)}/cars/{carId:length(24)}/service-rules")]
    public class ServiceRulesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ServiceRulesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [NSwag.Annotations.SwaggerResponse((int)HttpStatusCode.OK, typeof(IEnumerable<TimeServiceRuleVm>))]
        [NSwag.Annotations.SwaggerResponse((int)HttpStatusCode.OK, typeof(IEnumerable<MileageServiceRuleVm>))]
        [HttpGet]
        public async Task<IEnumerable<ServiceRuleVm>> GetList(string id, string carId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new ListServiceRulesForCarQuery { FleetId = id, CarId = carId }, cancellationToken);
        }
        [HttpPost("time")]
        public async Task<string> PostTime(string id, string carId, CreateTimeServiceRuleForCar.Dto dto, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreateTimeServiceRuleForCar { FleetId = id, CarId = carId, Data = dto }, cancellationToken);
        }
        [HttpPost("mileage")]
        public async Task<string> PostMileage(string id, string carId, CreateMileageServiceRuleForCar.Dto dto, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreateMileageServiceRuleForCar { FleetId = id, CarId = carId, Data = dto }, cancellationToken);
        }
        [HttpDelete("{serviceRuleId:length(24)}")]
        public async Task PostCar(string id, string carId, string serviceRuleId, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteServiceRuleForCar { FleetId = id, CarId = carId, SerivceRuleId = serviceRuleId }, cancellationToken);
        }
    }
}
