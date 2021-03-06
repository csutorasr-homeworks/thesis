﻿using Flottapp.Application.Account.Exceptions;
using Flottapp.Application.Fleet.Exceptions;
using Flottapp.Infrastucture;
using Flottapp.Infrastucture.Commands;
using Flottapp.Infrastucture.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FleetsController : ControllerBase
    {
        private readonly IMediator mediator;

        public FleetsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<FleetRowVm>> GetList(CancellationToken cancellationToken)
        {
            return await mediator.Send(new ListFleetsQuery(), cancellationToken);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<FleetVm> Get(string id, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetFleetQuery { Id = id }, cancellationToken);
        }
        [HttpPost]
        public async Task<string> Post(CreateFleetCommand command, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);
        }
        [HttpPut("{id:length(24)}")]
        public async Task Put(string id, ModifyFleetCommand.Dto dto, CancellationToken cancellationToken)
        {
            await mediator.Send(new ModifyFleetCommand { Id = id, Data = dto }, cancellationToken);
        }
        [HttpDelete("{id:length(24)}")]
        public async Task Delete(string id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteFleetCommand { Id = id }, cancellationToken);
        }
        [HttpPost("{id:length(24)}/users")]
        public async Task<IActionResult> AddUser(string id, AddUserToFleetCommand.Dto dto, CancellationToken cancellationToken)
        {
            try
            {
                await mediator.Send(new AddUserToFleetCommand { Id = id, Data = dto }, cancellationToken);
                return Ok();
            }
            catch (UserProfileNotFoundException)
            {
                return NotFound("User profile not found.");
            }
            catch (FleetUserAlreadyAddedException)
            {
                return Conflict("User has been already added to the fleet.");
            }
        }
        [HttpDelete("{id:length(24)}/users/{userId}")]
        public async Task RemoveUser(string id, string userId, CancellationToken cancellationToken)
        {
            await mediator.Send(new RemoveUserFromFleetCommand { Id = id, UserId = userId }, cancellationToken);
        }
    }
}
