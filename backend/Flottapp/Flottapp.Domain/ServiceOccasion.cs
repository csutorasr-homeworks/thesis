using System;

namespace Flottapp.Domain
{
    public class ServiceOccasion
    {
        public decimal Milage { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public ServiceType Type { get; set; }
    }
}