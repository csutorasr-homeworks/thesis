using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Application.Fleet.Exceptions
{
    [Serializable]
    public class FleetNotFoundException : Exception
    {
        public FleetNotFoundException() { }
        public FleetNotFoundException(string message) : base(message) { }
        public FleetNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected FleetNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
