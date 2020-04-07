using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastucture.Queries
{
    public class ListPaymentsForCarQuery : IRequest<PaymentRowVm>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
    }
}
