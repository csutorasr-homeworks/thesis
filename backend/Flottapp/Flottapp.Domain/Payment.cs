namespace Flottapp.Domain
{
    public class Payment
    {
        public Money Sum { get; set; }
        public bool Accepted { get; set; } = false;
    }
}