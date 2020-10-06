using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Domain;

namespace Flottapp.Application.Payments
{
    public interface IPaymentsStore
    {
        Task CreatePayment(string fleetId, string carId, Money price, DateTimeOffset now, CancellationToken cancellationToken);
        Task<IEnumerable<Payment>> GetPaymentsForCar(string fleetId, string carId, CancellationToken cancellationToken);
        Task AcceptPaymentForCar(string fleetId, string carId, string paymentId, DateTimeOffset now, CancellationToken cancellationToken);
    }
}
