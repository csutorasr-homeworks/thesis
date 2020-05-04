using Flottapp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Application.Providers
{
    class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
