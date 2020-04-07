using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateServiceOccasionForCarCommand : IRequest<string>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public decimal Mileage { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public ServiceType Type { get; set; }
    }
}
