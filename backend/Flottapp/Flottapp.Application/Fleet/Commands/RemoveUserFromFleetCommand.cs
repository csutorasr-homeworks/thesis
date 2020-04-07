using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class RemoveUserFromFleetCommand : IRequest
    {
        public string Id { get; set; }
        public string UserId { get; set; }
    }
}
