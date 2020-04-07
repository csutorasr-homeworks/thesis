using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.ViewModels
{
    public class TimeServiceRuleVm : ServiceRuleVm
    {
        public override ServiceRuleType Type { get; protected set; } = ServiceRuleType.Time;
        public int IntervalInMonth { get; set; }
    }
}
