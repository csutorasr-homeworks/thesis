using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateMileageServiceRuleForCar : IRequest<string>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public decimal TravelledMileage { get; set; }
    }
}
