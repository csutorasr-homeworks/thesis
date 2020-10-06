using Flottapp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Application.Providers
{
    class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}
