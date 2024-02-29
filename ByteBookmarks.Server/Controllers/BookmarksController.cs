#region

using System.Security.Claims;
using ByteBookmarks.Application.Bookmarks.Commands;
using ByteBookmarks.Application.Bookmarks.DTOs;
using ByteBookmarks.Application.Bookmarks.Queries;

#endregion

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookmarksController(IMediator mediator, DataContext context) : ControllerBase
{
    // GET: api/Bookmarks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetBookmarks()
    {
        try
        {
            var id = User?.FindFirstValue("userId");
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

        if (bookmark == null) return NotFound();

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
    public async Task<IActionResult> AddTagToBookmark(int bookmarkId, [FromBody] AddTagToBookmarkCommand command)
    {
        if (bookmarkId != command.BookmarkId) return BadRequest();

        if (command.UserId != User.FindFirstValue("userId")) return Unauthorized();

        try
        {
            await mediator.Send(command);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookmarkExists(bookmarkId))
                return NotFound();
            throw;
        }

        return NoContent();
    }
}