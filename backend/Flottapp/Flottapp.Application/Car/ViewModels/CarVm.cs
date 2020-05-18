namespace Flottapp.Infrastucture
{
    public class CarVm
    {
        public string Id { get; set; }
        public string LicensePlateNumber { get; set; }
        public MoneyVm LimitPerMonth { get; set; }
        public bool Activated { get; set; }
        public bool NeedsToBeServiced { get; set; }
    }
}