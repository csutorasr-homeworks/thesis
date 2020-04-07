using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateCarForFleetCommand : IRequest<string>
    {
        public string FleetId { get; set; }
        public Money LimitPerMonth { get; set; }
        public string LicensePlateNumber { get; set; }
    }
}
