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
            .Where(x => x.Id == request.CommentId && x.User.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (comment is null)
            return false;

        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}