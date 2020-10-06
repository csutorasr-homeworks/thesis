using System;

namespace Flottapp.Infrastucture
{
    public class PaymentRowVm
    {
        public string Id { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public MoneyVm Sum { get; set; }
        public DateTimeOffset? Accepted { get; set; }
    }
}