namespace Flottapp.Infrastucture
{
    public class MonthlyAggregateRowVm
    {
        public MoneyVm SumOfPrice { get; set; }
        public MoneyVm Limit { get; set; }
        public decimal TravelledDistance { get; set; }
        public bool Accepted { get; set; }
    }
}