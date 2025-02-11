using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Commands.Posts.DeletePost;

public class DeletePostCommandHandler(IDbContext dbContext) : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts.Where(x => x.Id == request.PostId && x.User.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);
        if (post is null)
            return false;

        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}