using System.Collections.Generic;

namespace Flottapp.Domain
{
    public class MontlyAggregate
    {
        public string Id { get; set; }
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Money Limit { get; set; }
        public bool Accepted { get; set; } = false;
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}