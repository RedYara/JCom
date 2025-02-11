using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Commands.Comments.LeaveComment;

public class LeaveCommentCommandHandler(IDbContext dbContext) : IRequestHandler<LeaveCommentCommand, bool>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<bool> Handle(LeaveCommentCommand request, CancellationToken cancellationToken)
    {
        Comment comment = new()
        {
            CommentDate = DateTime.UtcNow,
            Post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken: cancellationToken),
            Text = request.Text,
            User = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserTag == request.UserTag, cancellationToken: cancellationToken)
        };

        await _dbContext.Comments.AddAsync(comment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}