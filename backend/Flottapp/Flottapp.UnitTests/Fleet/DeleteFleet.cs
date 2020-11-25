
using System;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Application.Fleet;
using Flottapp.Application.Fleet.Exceptions;
using Flottapp.Infrastucture.Commands;
using Moq;
using Xunit;

namespace Flottapp.UnitTests
{
    public class DeleteFleet
    {
        private readonly Mock<IFleetStore> fleetStoreMock;
        private readonly DeleteFleetCommand.Handler handler;

        public DeleteFleet()
        {
            fleetStoreMock = new Mock<IFleetStore>();
            handler = new DeleteFleetCommand.Handler(fleetStoreMock.Object);
        }

        [Fact]
        public async Task DeleteFleetSuccess()
        {
            var cancellationToken = new CancellationToken();
            fleetStoreMock.Setup(x => x.DeleteFleet("1234", It.Is<Model.AuthorizationData>(x => x.Authority == "test-authority" && x.Id == "123"), cancellationToken));
            await handler.Handle(new DeleteFleetCommand
            {
                AuthorizationData = new Model.AuthorizationData
                {
                    Authority = "test-authority",
                    Id = "123",
                },
                Id = "1234",
            }, cancellationToken);
        }

        [Fact]
        public async Task DeleteFleetFailure()
        {
            var cancellationToken = new CancellationToken();
            fleetStoreMock.Setup(x => x.DeleteFleet("1234", It.Is<Model.AuthorizationData>(x => x.Authority == "test-authority" && x.Id == "123"), cancellationToken)).ThrowsAsync(new FleetNotFoundException());
            await Assert.ThrowsAsync<FleetNotFoundException>(async () => await handler.Handle(new DeleteFleetCommand
            {
                AuthorizationData = new Model.AuthorizationData
                {
                    Authority = "test-authority",
                    Id = "123",
                },
                Id = "1234",
            }, cancellationToken));
        }
    }
}
