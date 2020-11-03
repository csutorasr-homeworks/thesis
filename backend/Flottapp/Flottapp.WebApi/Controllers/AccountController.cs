using Flottapp.Application.Account;
using Flottapp.Application.Account.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Flottapp.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAuthConfig([Required]string account)
        {
            if (account == "none")
            {
                return BadRequest();
            }
            return Ok(new
            {
                Authority = "https://localhost:44336",
                Client_id = "flottapp",
            });
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var result = await mediator.Send(new GetProfileQuery());
                return Ok(result);
            }
            catch (UserProfileNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("profile")]
        public async Task<IActionResult> SetProfile([Required]SetProfileCommand.Dto data)
        {
            await mediator.Send(new SetProfileCommand
            {
                Data = data
            });
            return Ok();
        }
    }
}
