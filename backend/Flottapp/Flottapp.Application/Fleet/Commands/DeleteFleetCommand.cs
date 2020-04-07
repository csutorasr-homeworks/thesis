using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Commands
{
    public class DeleteFleetCommand : IRequest
    {
        public string Id { get; set; }
    }
}
