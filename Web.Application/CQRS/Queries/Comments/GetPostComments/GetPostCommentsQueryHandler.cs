using Domain;
using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Comments.GetPostComments;

public class GetPostCommentsQueryHandler(IDbContext dbContext) : IRequestHandler<GetPostCommentsQuery, List<GetPostCommentsVm>>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<List<GetPostCommentsVm>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _dbContext.Comments
            .Include(x => x.User)
                .ThenInclude(x => x.UserImage)
            .Include(x => x.Post)
                .ThenInclude(x => x.User)
            .Where(x => x.Post.Id == request.PostId)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        var result = comments.Select(x =>
            new GetPostCommentsVm()
            {
                CommentDate = x.CommentDate.ToString(),
                CommentDateHumanized = (-(DateTime.UtcNow - x.CommentDate)).Humanize(),
                Text = x.Text,
                UserImagePath = x.User.UserImage.Path,
                UserName = x.User.UserName,
                UserId = x.User.Id,
                CommentId = x.Id,
                PostId = x.Post.Id,
                UserPostedId = x.Post.User.Id
            }).ToList();

        return result ?? [];
    }
}