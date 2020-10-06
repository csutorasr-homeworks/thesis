using Flottapp.Domain;
using MediatR;

namespace Flottapp.Application.Car
{
    public class CarLimitChangedEvent : INotification
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public Money Limit { get; set; }
    }
}
