using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class DeleteRegistrationForCarCommand : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public string RegistrationId { get; set; }
    }
}
