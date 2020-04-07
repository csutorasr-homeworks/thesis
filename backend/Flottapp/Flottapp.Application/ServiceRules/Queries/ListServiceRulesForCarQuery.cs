using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Queries
{
    public class ListServiceRulesForCarQuery : IRequest<IEnumerable<ServiceRuleVm>>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
    }
}
