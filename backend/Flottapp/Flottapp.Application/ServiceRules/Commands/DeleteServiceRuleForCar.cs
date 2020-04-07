using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class DeleteServiceRuleForCar : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public string SerivceRuleId { get; set; }
    }
}
