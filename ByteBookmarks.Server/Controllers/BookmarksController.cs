#region

using System.Security.Claims;
using ByteBookmarks.Application.Bookmarks.Commands;
using ByteBookmarks.Application.Bookmarks.DTOs;
using ByteBookmarks.Application.Bookmarks.Queries;
using ByteBookmarks.Core.Interfaces;

#endregion

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookmarksController(IMediator mediator, DataContext context, IUserService userService) : ControllerBase
{
    // GET: api/Bookmarks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetBookmarks()
    {
        try
        {
            var id = User?.FindFirstValue("userId");
            if (id == null) return Unauthorized();
            var query = new GetBookmarksQuery(id);
            var bookmarks = await mediator.Send(query);


            return Ok(bookmarks);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // GET: api/Bookmarks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookmarkDto>> GetBookmark(int id)
    {
        var query = new GetBookmarkByIdQuery(id, User.FindFirstValue("userId"));
        var bookmark = await mediator.Send(query);

        return Ok(bookmark);
    }

    // POST: api/Bookmarks
    [HttpPost]
    public async Task<ActionResult<BookmarkDto>> CreateBookmark(NewBookmarkDto newBookmark)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        // Create command
        var command = new CreateBookmarkCommand
        {
            Title = newBookmark.Title,
            URL = newBookmark.URL,
            Description = newBookmark.Description,
            IsPasswordProtected = newBookmark.IsPasswordProtected,
            Password = newBookmark.Password,
            Image = newBookmark.Image,
            UserId = User.FindFirstValue("userId")
        };
        var createdBookmark = await mediator.Send(command);

        return CreatedAtAction("GetBookmark", new { id = createdBookmark.Id }, createdBookmark);
    }

    // PUT: api/Bookmarks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookmark(int id, UpdateBookmarkCommand command)
    {
        if (id != command.Id) return BadRequest();

        command.UserId = User.FindFirstValue("userId");

        try
        {
            await mediator.Send(command);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookmarkExists(id)) // Add helper method
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Bookmarks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookmark(int id)
    {
        var command = new DeleteBookmarkCommand(id, User.FindFirstValue("userId"));

        try
        {
            await mediator.Send(command);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookmarkExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // Helper method
    private bool BookmarkExists(int id)
    {
        // Implement logic using your DataContext to check if the bookmark exists
        return context.Bookmarks.Any(b => b.Id == id);
    }

    // Add tag to bookmark
    [HttpPost("{bookmarkId}/tag")]
    public async Task<IActionResult> AddTagToBookmark(int bookmarkId, [FromBody] BookmarkTagDto NewBookmarkDto)
    {
        var command = new AddTagToBookmarkCommand
        {
            BookmarkId = bookmarkId,
            TagName = NewBookmarkDto.Name
        };
        await mediator.Send(command);

        // Return create at action
        return CreatedAtAction("GetBookmark", new { id = bookmarkId }, NewBookmarkDto);
    }

    // GET: api/Bookmarks?page=1&pageSize=10
    [HttpGet("paginated")]
    public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetBookmarksByPageAndSize([FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetBookmarksWithPaginationQuery(User.FindFirstValue("userId"), page, pageSize);
        var bookmarks = await mediator.Send(query);
        return Ok(bookmarks);
    }

    // GET: api/Bookmarks/userId
    [HttpGet("{userId?}")] // The ":int?" makes the userId parameter optional and of type int
    public async Task<ActionResult<List<BookmarkDto>>> GetUserBookmarks(string? userId)
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

        var query = new GetBookmarksByUserIdQuery(targetUserId);
        var bookmarks = await mediator.Send(query);
        return Ok(bookmarks);
    }

    // Delete tag from bookmark
    [HttpDelete("{bookmarkId}/tag/{tagId}")]
    public async Task<IActionResult> DeleteTagFromBookmark(int bookmarkId, int tagId)
    {
        var command = new DeleteBookmarkTagCommand
        {
            BookmarkId = bookmarkId,
            TagId = tagId,
            UserId = User.FindFirstValue("userId")
        };
        var result = await mediator.Send(command);

        return result ? Ok() : Forbid();
    }
}