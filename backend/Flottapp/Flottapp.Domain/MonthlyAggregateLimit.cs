namespace Flottapp.Domain
{
    public class MonthlyAggregateLimit
    {
        public string Id { get; set; }
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public Money Limit { get; set; }
    }
}