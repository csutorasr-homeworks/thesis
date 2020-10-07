using System;

namespace Flottapp.Domain
{
    public class ServiceOccasion
    {
        public string Id { get; set; }
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public decimal Mileage { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public ServiceType Type { get; set; }
    }
}