using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Commands.Comments.DeleteComment;

public class DeleteCommentCommandHandler(IDbContext dbContext) : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _dbContext.Comments
            .Include(x => x.User)
            .Include(x => x.Post)
            .Where(x => x.Id == request.CommentId)
            .FirstOrDefaultAsync(cancellationToken);

        if (comment is null)
            return false;

        if (comment.Post.Id == request.PostId || comment.User.UserTag == request.UserTag)
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}