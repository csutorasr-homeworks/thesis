﻿namespace Flottapp.Infrastucture
{
    public class MonthlyAggregateRowVm
    {
        public Money SumOfPrice { get; set; }
        public Money Limit { get; set; }
        public decimal TravelledDistance { get; set; }
        public bool Accepted { get; set; }
    }
}