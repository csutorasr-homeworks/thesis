using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.ViewModels
{
    public class MileageServiceRuleVm : ServiceRuleVm
    {
        public override ServiceRuleType Type { get; protected set; } = ServiceRuleType.Mileage;
        public decimal TravelledMileage { get; set; }
    }
}
