using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Domain
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
