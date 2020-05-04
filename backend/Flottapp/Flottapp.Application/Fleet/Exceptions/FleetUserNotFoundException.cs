using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Application.Fleet.Exceptions
{

    [Serializable]
    public class FleetUserNotFoundException : Exception
    {
        public FleetUserNotFoundException() { }
        public FleetUserNotFoundException(string message) : base(message) { }
        public FleetUserNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected FleetUserNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
