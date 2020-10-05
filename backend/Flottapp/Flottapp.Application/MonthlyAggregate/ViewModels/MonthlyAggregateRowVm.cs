namespace Flottapp.Infrastucture
{
    public class MonthlyAggregateRowVm
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public MoneyVm SumOfPrice { get; set; }
        public MoneyVm Limit { get; set; }
        public decimal TravelledDistance { get; set; }
        public bool Accepted { get; set; }
    }
}