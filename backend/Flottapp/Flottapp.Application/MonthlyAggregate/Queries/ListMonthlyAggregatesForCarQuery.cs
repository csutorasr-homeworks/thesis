using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Queries
{
    public class ListMonthlyAggregatesForCarQuery : IRequest<IEnumerable<MonthlyAggregateRowVm>>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public int PageSize { get; set; }
        public int PageLength { get; set; }
    }
}
