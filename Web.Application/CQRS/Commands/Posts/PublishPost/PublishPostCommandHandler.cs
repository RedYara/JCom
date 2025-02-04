using Domain;
using MediatR;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Commands.Posts.PublishPost;

public class PublishPostCommandHandler(IDbContext dbContext) : IRequestHandler<PublishPostCommand>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task Handle(PublishPostCommand request, CancellationToken cancellationToken)
    {
        Post post = new()
        {
            PostDate = DateTime.UtcNow,
            Text = request.Text,
            User = request.User
        };

        await _dbContext.Posts.AddAsync(post, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}