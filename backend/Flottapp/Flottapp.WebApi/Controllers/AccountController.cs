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
    }
}
