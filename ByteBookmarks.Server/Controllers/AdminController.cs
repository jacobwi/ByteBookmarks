#region

using ByteBookmarks.Application.Admin.Queries;
using ByteBookmarks.Application.Users.Commands;
using ByteBookmarks.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ByteBookmarks.Server.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Admin/Users
    [HttpGet("Users")]
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
    {
        var query = new GetAllUsersQuery();
        var users = await _mediator.Send(query);
        return Ok(users);
    }

    // GET: api/Admin/User/5
    [HttpGet("User/{id}")]
    public async Task<ActionResult<ApplicationUser>> GetUser(string id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query);

        return Ok(user);
    }

    // PUT: api/Admin/User/5 (Example: Update user role)
    [HttpPut("User/{id}")]
    public async Task<ActionResult> UpdateUserRole(string id, [FromBody] UpdateUserRoleCommand command)
    {
        if (id != command.UserId) return BadRequest("User ID mismatch");

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Admin/User/5
    [HttpDelete("User/{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        var command = new DeleteUserCommand(id);

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}