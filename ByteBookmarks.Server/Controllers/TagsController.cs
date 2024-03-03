#region

using ByteBookmarks.Application.Tags;
using ByteBookmarks.Application.Tags.Commands;
using ByteBookmarks.Application.Tags.Queries;
using ByteBookmarks.Core.Interfaces;

#endregion

namespace ByteBookmarks.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController(IMediator mediator, IUserService userService) : ControllerBase
{
    // GET: api/Tags
    [HttpGet("{userId?}")] // The ":int?" makes the userId parameter optional and of type int
    public async Task<ActionResult<IEnumerable<TagDto>>> GetUserTags(string? userId)
    {
        var currentUserId = userService.GetCurrentUserId();
        if (string.IsNullOrEmpty(currentUserId)) return Unauthorized();

        // If no userId is provided in the route, use the current user's ID
        var targetUserId = userId ?? currentUserId;

        // Check if the user is requesting someone else's bookmarks
        if (targetUserId != currentUserId)
            // Ensure the requester is an admin if they're requesting bookmarks for a different user
            if (!User.IsInRole("Admin"))
                return Forbid(); // Or return a custom error message

        var query = new GetTagsByUserIdQuery(targetUserId);
        var tags = await mediator.Send(query);
        return Ok(tags);
    }

    // GET: api/Tags/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> GetTag(int id)
    {
        var query = new GetTagByIdQuery(id);
        var tag = await mediator.Send(query);

        if (tag == null) return NotFound();

        return Ok(tag);
    }

    // PUT: api/Tags/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTag(int id, TagDto tag)
    {
        if (id != tag.Id) return BadRequest();

        var command = new UpdateTagCommand
        {
            Id = tag.Id,
            Name = tag.Name
        };

        await mediator.Send(command);

        return NoContent();
    }

    // POST: api/Tags
    [HttpPost]
    public async Task<ActionResult<TagDto>> PostTag(TagDto tag)
    {
        var command = new CreateTagCommand
        {
            Name = tag.Name,
            UserId = tag.UserId
        };

        var createdTag = await mediator.Send(command);

        return CreatedAtAction("GetTag", new { id = createdTag.Id }, createdTag);
    }

    // DELETE: api/Tags/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        var command = new DeleteTagCommand { Id = id };
        await mediator.Send(command);

        return NoContent();
    }
}