using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class ListFleetsQuery : IRequest<IEnumerable<FleetRowVm>>
    {
        public class ListFleetsQueryHandler : IRequestHandler<ListFleetsQuery, IEnumerable<FleetRowVm>>
        {
            public async Task<IEnumerable<FleetRowVm>> Handle(ListFleetsQuery request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(new List<FleetRowVm> {
                    new FleetRowVm { Id = "1", Name = "Test" }
                });
            }
        }
    }
}
