using System;

namespace Flottapp.Domain
{
    public class Payment
    {
        public string Id { get; set; }
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public Money Sum { get; set; }
        public DateTimeOffset? Accepted { get; set; } = null;
    }
}