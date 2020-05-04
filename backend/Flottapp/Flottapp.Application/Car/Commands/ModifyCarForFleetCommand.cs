using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class ModifyCarForFleetCommand : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public MoneyVm LimitPerMonth { get; set; }
        public string LicensePlateNumber { get; set; }
    }
}
