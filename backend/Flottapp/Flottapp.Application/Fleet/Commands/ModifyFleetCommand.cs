using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class ModifyFleetCommand : IRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
