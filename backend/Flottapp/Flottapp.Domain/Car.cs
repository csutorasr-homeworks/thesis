using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flottapp.Domain
{
    public class Car
    {
        public string Id { get; set; }
        public string LicensePlateNumber { get; set; }
        public bool Activated { get; set; }
        public Money LimitPerMonth { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<MontlyAggregate> MontlyAggregates { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<ServiceOccasion> ServiceOccasions { get; set; }
        public ICollection<ServiceRule> ServiceRules { get; set; }
    }
}
