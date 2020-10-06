using System;
namespace Flottapp.Application.MonthlyAggregate
{

    [Serializable]
    public class MonthlyAggregateAlreadyAcceptedException : Exception
    {
        public MonthlyAggregateAlreadyAcceptedException() { }
        public MonthlyAggregateAlreadyAcceptedException(string message) : base(message) { }
        public MonthlyAggregateAlreadyAcceptedException(string message, Exception inner) : base(message, inner) { }
        protected MonthlyAggregateAlreadyAcceptedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
