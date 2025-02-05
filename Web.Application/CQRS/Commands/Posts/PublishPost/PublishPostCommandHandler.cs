using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Commands.Posts.PublishPost;

public class PublishPostCommandHandler(IDbContext dbContext) : IRequestHandler<PublishPostCommand, int>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<int> Handle(PublishPostCommand request, CancellationToken cancellationToken)
    {
        Post post = new()
        {
            PostDate = DateTime.UtcNow,
            Text = request.Text,
            User = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
        };

        await _dbContext.Posts.AddAsync(post, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}