using System;

namespace Flottapp.Domain
{
    public class Registration
    {
        public string Id { get; set; }
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public decimal Mileage { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public Location Location { get; set; }
        public decimal RefuelQuantity { get; set; }
        public Money Price { get; set; }
    }
}