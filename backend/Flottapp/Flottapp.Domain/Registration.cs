using System;

namespace Flottapp.Domain
{
    public class Registration
    {
        public decimal Mileage { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public Location Location { get; set; }
        public decimal RefuelQuantity { get; set; }
        public Money Price { get; set; }
    }
}