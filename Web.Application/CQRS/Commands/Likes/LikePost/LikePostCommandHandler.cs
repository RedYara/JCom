using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Commands.Likes.LikePost;

public class LikePostCommandHandler(IDbContext dbContext) : IRequestHandler<LikePostCommand, bool>
{
    private readonly IDbContext _dbContext = dbContext;

    public async Task<bool> Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts.Where(x => x.Id == request.PostId).FirstOrDefaultAsync(cancellationToken);
        if (post == null)
            return false;

        var user = await _dbContext.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync();
        if (user == null)
            return false;

        try
        {
            var likedPost = await _dbContext.Likes.Where(x => x.Post == post && x.User == user).FirstOrDefaultAsync(cancellationToken);
            if (likedPost is null)
                await _dbContext.Likes.AddAsync(new()
                {
                    Post = post,
                    User = user
                }, cancellationToken);

            else
                _dbContext.Likes.Remove(likedPost);

            await _dbContext.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }

        return true;
    }

}