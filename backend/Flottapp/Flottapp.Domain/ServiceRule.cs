namespace Flottapp.Domain
{
    public abstract class ServiceRule
    {
        public string Id { get; set; }
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public abstract bool NeedsService(Car car, IDateTimeProvider dateTimeProvider);
    }
}