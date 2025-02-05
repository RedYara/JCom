using Domain;
using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Comments.GetUserComments;

public class GetUserCommentsQueryHandler(IDbContext dbContext) : IRequestHandler<GetUserCommentsQuery, List<GetUserCommentsVm>>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<List<GetUserCommentsVm>> Handle(GetUserCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _dbContext.Comments
            .Include(x => x.User)
                .ThenInclude(x => x.UserImage)
            .Where(x => x.User.Id == request.UserId)
            .Take(3)
            .ToListAsync(cancellationToken);

        var result = comments.Select(x => new GetUserCommentsVm
        {
            CommentDate = x.CommentDate.ToString(),
            CommentDateHumanized = (-(DateTime.UtcNow - x.CommentDate)).Humanize(),
            Text = x.Text,
            UserImagePath = x.User.UserImage.Path,
            UserName = x.User.UserName
        }).ToList();

        return result;
    }
}