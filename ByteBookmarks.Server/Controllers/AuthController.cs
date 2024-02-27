#region

using ByteBookmarks.Application.Authentication;
using ByteBookmarks.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
using AuthenticationResponse = ByteBookmarks.Application.Authentication.AuthenticationResponse;

#endregion

namespace ByteBookmarks.Server.Controllers;

public class AuthController(IMediator mediator, IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService; // For additional utilities if needed

    // POST: api/Auth/login
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoginDto userLogin)

    {
        try
        {
            var loginCommand = TinyMapper.Map<LoginUserCommand>(userLogin);

            var response = await mediator.Send(loginCommand);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(); // Or a more informative error response
        }
    }

    // POST: api/Auth/register
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResponse>> Register([FromBody] SignupDto newUser)
    {
        try
        {
            var signupCommand = TinyMapper.Map<RegisterUserCommand>(newUser);

            var response = await mediator.Send(signupCommand);
            return Ok(response); // Consider returning CreatedAtAction
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Or a more informative error response
        }
    }
}