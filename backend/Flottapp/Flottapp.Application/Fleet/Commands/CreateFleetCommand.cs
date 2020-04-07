using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateFleetCommand : IRequest<string>
    {
        public string Name { get; set; }
    }
}
