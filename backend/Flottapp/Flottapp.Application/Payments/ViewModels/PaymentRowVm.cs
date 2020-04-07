namespace Flottapp.Infrastucture
{
    public class PaymentRowVm
    {
        public string Id { get; set; }
        public Money Sum { get; set; }
        public bool Accepted { get; set; }
    }
}