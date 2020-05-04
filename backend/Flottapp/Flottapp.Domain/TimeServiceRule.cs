using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flottapp.Domain
{
    public class TimeServiceRule : ServiceRule
    {
        public int IntervalInMonth { get; set; }

        public override bool NeedsService(Car car, IDateTimeProvider dateTimeProvider)
        {
            var lastServiceTime = car.ServiceOccasions.OrderBy(x => x.CreationTime).LastOrDefault()?.CreationTime ?? car.CreationTime;
            return lastServiceTime.AddMonths(IntervalInMonth) < dateTimeProvider.Now();
        }
    }
}
