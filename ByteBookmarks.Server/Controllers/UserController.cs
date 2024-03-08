#region

using System.Security.Claims;
using ByteBookmarks.Application.Users.DTOs;
using ByteBookmarks.Application.Users.Queries;
using Nelibur.ObjectMapper;

#endregion

namespace ByteBookmarks.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    // Get api/User/{userId}/profile
    [HttpGet("{userId}/profile")]
    public async Task<ActionResult<UserProfileDto>> GetUserProfile(string userId)
    {
        try
        {
            var id = User?.FindFirstValue("userId");
            if (id == null) return Unauthorized();

            if (id != userId) return Unauthorized();

            var query = new GetUserProfileByIdQuery(userId);
            var response = await mediator.Send(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Or a more informative error response
        }
    }

    // PUT api/User/{userId}/profile
    [HttpPut("{userId}/profile")]
    public async Task<ActionResult<UserProfileDto>> UpdateUserProfile(string userId,
        [FromBody] UserProfileDto updatedProfile)
    {
        try
        {
            var id = User?.FindFirstValue("userId");
            if (id == null) return Unauthorized();

            if (id != userId) return Unauthorized();

            var command = TinyMapper.Map<UpdateProfileForUserCommand>(updatedProfile);
            command.UserId = userId;
            var response = await mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Or a more informative error response
        }
    }

    // DELETE api/User/{userId}/profile
    [HttpDelete("{userId}/profile")]
    public async Task<ActionResult> DeleteUserProfile(string userId)
    {
        try
        {
            var id = User?.FindFirstValue("userId");
            if (id == null) return Unauthorized();

            if (id != userId) return Unauthorized();

            var command = new DeleteProfileFromUserCommand(userId);
            var response = await mediator.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Or a more informative error response
        }
    }
}