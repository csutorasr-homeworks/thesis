using Flottapp.Infrastucture;
using Flottapp.Infrastucture.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FleetsController : ControllerBase
    {
        private readonly IMediator mediator;

        public FleetsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<FleetRowVm>> Get(CancellationToken cancellationToken)
        {
            return await mediator.Send(new ListFleetsQuery(), cancellationToken);
        }
    }
}
