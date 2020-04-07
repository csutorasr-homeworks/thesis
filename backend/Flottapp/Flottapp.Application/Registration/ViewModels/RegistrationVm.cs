using System;

namespace Flottapp.Infrastucture
{
    public class RegistrationVm
    {
        public DateTimeOffset Time { get; set; }
        public decimal Mileage { get; set; }
        public Location Location { get; set; }
        public decimal RefuelQuantity { get; set; }
        public Money Price { get; set; }
    }
}