using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateRegistrationForCarCommand : IRequest<string>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public DateTimeOffset Time { get; set; }
        public decimal Mileage { get; set; }
        public Location Location { get; set; }
        public decimal RefuelQuantity { get; set; }
        public Money Price { get; set; }
    }
}
