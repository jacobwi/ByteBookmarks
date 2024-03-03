namespace ByteBookmarks.Application.Tags.Commands;

public class CreateTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<CreateTagCommand, TagDto>
{
    public async Task<TagDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new Tag
        {
            Name = request.Name,
            UserId = request.UserId
        };

        await tagRepository.CreateTag(entity);

        return new TagDto
        {
            Id = entity.TagId,
            Name = entity.Name,
            UserId = entity.UserId
        };
    }
}