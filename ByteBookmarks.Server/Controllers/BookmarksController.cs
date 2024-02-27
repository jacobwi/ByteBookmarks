#region

using System.Security.Claims;
using ByteBookmarks.Application.Bookmarks.Commands;
using ByteBookmarks.Application.Bookmarks.DTOs;
using ByteBookmarks.Application.Bookmarks.Queries;
using ByteBookmarks.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var query = new GetBookmarksQuery(User.Identity.Name);
        var bookmarks = await mediator.Send(query);
        return Ok(bookmarks);
    }

    // GET: api/Bookmarks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookmarkDto>> GetBookmark(int id)
    {
        var query = new GetBookmarkByIdQuery(id, User.Identity.Name);
        var bookmark = await mediator.Send(query);

        if (bookmark == null) return NotFound();

        return Ok(bookmark);
    }

    // POST: api/Bookmarks
    [HttpPost]
    public async Task<ActionResult<BookmarkDto>> CreateBookmark(CreateBookmarkCommand command)
    {
        command.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var createdBookmark = await mediator.Send(command);

        return CreatedAtAction("GetBookmark", new { id = createdBookmark.Id }, createdBookmark);
    }

    // PUT: api/Bookmarks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookmark(int id, UpdateBookmarkCommand command)
    {
        if (id != command.Id) return BadRequest();

        command.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
        var command = new DeleteBookmarkCommand(id, User.FindFirstValue(ClaimTypes.NameIdentifier));

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
}