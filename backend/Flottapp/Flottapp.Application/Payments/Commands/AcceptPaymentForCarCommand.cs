using Flottapp.Application.Payments;
using Flottapp.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class AcceptPaymentForCarCommand : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public string PaymentId { get; set; }
        public class Handler : IRequestHandler<AcceptPaymentForCarCommand>
        {
            private readonly IPaymentsStore paymentsStore;
            private readonly IDateTimeProvider dateTimeProvider;

            public Handler(IPaymentsStore paymentsStore, IDateTimeProvider dateTimeProvider)
            {
                this.paymentsStore = paymentsStore;
                this.dateTimeProvider = dateTimeProvider;
            }
            public async Task<Unit> Handle(AcceptPaymentForCarCommand request, CancellationToken cancellationToken)
            {
                await paymentsStore.AcceptPaymentForCar(request.FleetId, request.CarId, request.PaymentId, dateTimeProvider.Now(), cancellationToken);
                return Unit.Value;
            }
        }
    }
}
