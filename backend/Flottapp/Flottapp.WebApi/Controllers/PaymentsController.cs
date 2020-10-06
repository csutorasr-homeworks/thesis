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
    [Route("api/fleets/{id:length(24)}/cars/{carId:length(24)}/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PaymentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<PaymentRowVm>> GetCar(string id, string carId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new ListPaymentsForCarQuery { FleetId = id, CarId = carId }, cancellationToken);
        }
        [HttpPost("{paymentId:length(24)}/approve")]
        public async Task PostCar(string id, string carId, string paymentId, CancellationToken cancellationToken)
        {
            await mediator.Send(new AcceptPaymentForCarCommand { FleetId = id, CarId = carId, PaymentId = paymentId }, cancellationToken);
        }
    }
}
