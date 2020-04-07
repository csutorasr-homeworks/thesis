using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Queries
{
    public class ListCarsForFleetQuery : IRequest<IEnumerable<CarRowVm>>
    {
        public string FleetId { get; set; }
        public string LicensePlateNumber { get; set; }
        public bool NeedsToBeServiced { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
