using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Application.Fleet.Exceptions
{

    [Serializable]
    public class FleetUserAlreadyAddedException : Exception
    {
        public FleetUserAlreadyAddedException() { }
        public FleetUserAlreadyAddedException(string message) : base(message) { }
        public FleetUserAlreadyAddedException(string message, Exception inner) : base(message, inner) { }
        protected FleetUserAlreadyAddedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
