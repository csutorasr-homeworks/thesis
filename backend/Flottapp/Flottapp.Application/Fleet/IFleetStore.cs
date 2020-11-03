using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Model;

namespace Flottapp.Application.Fleet
{
    public interface IFleetStore
    {
        Task<List<Domain.Fleet>> GetFleets(AuthorizationData authorizationData, CancellationToken cancellationToken);
        Task<string> CreateFleet(string name, AuthorizationData authorizationData, CancellationToken cancellationToken);
        Task DeleteFleet(string id, AuthorizationData authorizationData, CancellationToken cancellationToken);
        Task<Domain.Fleet> GetFleet(string id, AuthorizationData authorizationData, CancellationToken cancellationToken);
        Task SaveName(Domain.Fleet fleet, CancellationToken cancellationToken);
        Task AddUserToFleet(string id, AuthorizationData authorizationDataOfAddedUser, AuthorizationData authorizationData, CancellationToken cancellationToken);
        Task RemoveUserFromFleet(string id, AuthorizationData authorizationDataOfRemovedUser, AuthorizationData authorizationData, CancellationToken cancellationToken);
    }
}
