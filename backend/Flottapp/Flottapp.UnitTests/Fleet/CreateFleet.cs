using System;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Application.Fleet;
using Flottapp.Infrastucture.Commands;
using Moq;
using Xunit;

namespace Flottapp.UnitTests
{
    public class CreateFleet
    {
        private readonly Mock<IFleetStore> fleetStoreMock;
        private readonly CreateFleetCommand.Handler handler;

        public CreateFleet()
        {
            fleetStoreMock = new Mock<IFleetStore>();
            handler = new CreateFleetCommand.Handler(fleetStoreMock.Object);
        }

        [Fact]
        public async Task CreateFleetSuccess()
        {
            var cancellationToken = new CancellationToken();
            fleetStoreMock.Setup(x => x.CreateFleet("New fleet name", It.Is<Model.AuthorizationData>(x => x.Authority == "test-authority" && x.Id == "123"), cancellationToken)).ReturnsAsync("new Id");
            var result = await handler.Handle(new CreateFleetCommand
            {
                AuthorizationData = new Model.AuthorizationData
                {
                    Authority = "test-authority",
                    Id = "123",
                },
                Name = "New fleet name",
            }, cancellationToken);
            Assert.Equal("new Id", result);
        }
    }
}
