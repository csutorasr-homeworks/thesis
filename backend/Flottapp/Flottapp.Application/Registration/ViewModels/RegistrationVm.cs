using System;

namespace Flottapp.Infrastucture
{
    public class RegistrationVm
    {
        public string Id { get; set; }
        public DateTimeOffset Time { get; set; }
        public decimal Mileage { get; set; }
        public LocationVm Location { get; set; }
        public decimal RefuelQuantity { get; set; }
        public MoneyVm Price { get; set; }
    }
}