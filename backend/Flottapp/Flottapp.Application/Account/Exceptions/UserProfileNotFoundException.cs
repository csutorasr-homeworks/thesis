using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Application.Account.Exceptions
{
    [Serializable]
    public class UserProfileNotFoundException : Exception
    {
        public UserProfileNotFoundException() { }
        public UserProfileNotFoundException(string message) : base(message) { }
        public UserProfileNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected UserProfileNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
