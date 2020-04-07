using System;

namespace Flottapp.Infrastucture
{
    public class SerivceOccasionsVm
    {
        public string Id { get; set; }
        public decimal Mileage { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public ServiceType Type { get; set; }
    }
}