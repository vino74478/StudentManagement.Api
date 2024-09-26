using Application.Commands.Account;
using Application.DTO.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return Ok(await _mediator.Send(new RegisterCommand { Register = registerDto }));
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("User ID and token are required");
            }
            return Ok(await _mediator.Send(new ConfirmEmailCommand { userId = userId,token=code}));
            
        }
    }
}
