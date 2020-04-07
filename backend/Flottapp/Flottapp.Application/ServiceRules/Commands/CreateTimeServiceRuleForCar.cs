using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateTimeServiceRuleForCar : IRequest<string>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public int IntervalInMonth { get; set; }
    }
}
