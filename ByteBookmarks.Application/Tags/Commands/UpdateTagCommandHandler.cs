namespace ByteBookmarks.Application.Tags.Commands;

public class UpdateTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<UpdateTagCommand, TagDto>
{
    public async Task<TagDto> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await tagRepository.GetTag(request.Id, request.UserId);

        if (entity == null) throw new KeyNotFoundException(request.Id.ToString());

        entity.Name = request.Name;

        await tagRepository.UpdateTag(entity);

        return new TagDto
        {
            Id = entity.TagId,
            Name = entity.Name,
            UserId = entity.UserId
        };
    }
}