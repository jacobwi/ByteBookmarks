#region

using ByteBookmarks.Application.Categories.Commands;
using ByteBookmarks.Application.Categories.DTOs;
using ByteBookmarks.Application.Categories.Queries;
using ByteBookmarks.Core.Interfaces;

#endregion

namespace ByteBookmarks.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public CategoryController(IMediator mediator, IUserService userService)
    {
        _mediator = mediator;
        _userService = userService;
    }

    // GET: api/Category
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var currentUserId = _userService.GetCurrentUserId();
        if (string.IsNullOrEmpty(currentUserId)) return Unauthorized();

        var query = new GetCategoriesByUserIdQuery(currentUserId);
        var categories = await _mediator.Send(query);
        return Ok(categories);
    }

    // GET: api/Category/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var query = new GetCategoryByIdQuery(id);
        var category = await _mediator.Send(query);

        if (category == null) return NotFound();

        return Ok(category);
    }

    // PUT: api/Category/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, CategoryDto category)
    {
        if (id != category.Id) return BadRequest();

        var command = new UpdateCategoryCommand
        {
            Id = category.Id,
            Name = category.Name
        };

        await _mediator.Send(command);

        return NoContent();
    }

    // POST: api/Category
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto category)
    {
        var command = new CreateCategoryCommand
        {
            Name = category.Name,
            UserId = _userService.GetCurrentUserId()
        };

        var createdCategory = await _mediator.Send(command);

        return CreatedAtAction("GetCategory", new { id = createdCategory.Id }, createdCategory);
    }

    // DELETE: api/Category/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var command = new DeleteCategoryCommand
        {
            Id = id
        };

        await _mediator.Send(command);

        return NoContent();
    }

    // GET: api/Category?page=1&pageSize=10
    [HttpGet("paginated")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>>
        GetCategories([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var currentUserId = _userService.GetCurrentUserId();
        if (string.IsNullOrEmpty(currentUserId)) return Unauthorized();

        var query = new GetCategoriesWithPaginationByUserIdQuery(currentUserId, page, pageSize);
        var categories = await _mediator.Send(query);
        return Ok(categories);
    }


    private bool CategoryExists(int id)
    {
        var query = new GetCategoryByIdQuery(id);
        var category = _mediator.Send(query).Result;
        return category != null;
    }
}