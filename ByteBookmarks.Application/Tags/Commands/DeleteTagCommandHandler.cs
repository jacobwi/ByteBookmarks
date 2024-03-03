namespace ByteBookmarks.Application.Tags.Commands;

public class DeleteTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<DeleteTagCommand, bool>
{
    public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await tagRepository.GetTag(request.Id, request.UserId);

        if (entity == null) throw new KeyNotFoundException(request.Id.ToString());

        tagRepository.DeleteTag(entity.TagId, request.UserId);

        return true;
    }
}