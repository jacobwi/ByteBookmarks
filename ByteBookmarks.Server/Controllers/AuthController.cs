#region

using ByteBookmarks.Application.Authentication;
using ByteBookmarks.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthenticationResponse = ByteBookmarks.Application.Authentication.AuthenticationResponse;

#endregion

namespace ByteBookmarks.Server.Controllers;

public class AuthController(IMediator mediator, IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService; // For additional utilities if needed

    // POST: api/Auth/login
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoginUserCommand command)
    {
        try
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(); // Or a more informative error response
        }
    }

    // POST: api/Auth/register
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResponse>> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            var response = await mediator.Send(command);
            return Ok(response); // Consider returning CreatedAtAction
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Or a more informative error response
        }
    }
}