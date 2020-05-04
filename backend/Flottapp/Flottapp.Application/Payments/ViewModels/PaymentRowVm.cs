namespace Flottapp.Infrastucture
{
    public class PaymentRowVm
    {
        public string Id { get; set; }
        public MoneyVm Sum { get; set; }
        public bool Accepted { get; set; }
    }
}