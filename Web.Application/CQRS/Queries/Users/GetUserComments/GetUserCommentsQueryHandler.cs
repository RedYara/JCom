using Domain;
using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Users.GetUserComments;

public class GetUserPostsQueryHandler(IDbContext dbContext) : IRequestHandler<GetUserCommentsQuery, List<GetUserCommentsVm>>
{
    private IDbContext _dbContext = dbContext;
    public async Task<List<GetUserCommentsVm>> Handle(GetUserCommentsQuery query, CancellationToken cancellationToken)
    {
        // Получение последних 5 комментариев от пользователя
        var comments = await _dbContext.Comments
            .Include(x => x.User)
                .ThenInclude(x => x.UserImage)
            .Include(x => x.Post)
            .Where(x => x.User.Id == query.UserId)
            .OrderByDescending(x => x.Id)
            .Take(3)
            .ToListAsync(cancellationToken);

        // Преобразуем комменты в нужный формат
        var result = comments.Select(x => new GetUserCommentsVm()
        {
            LikesCount = x.Likes,
            CommentDate = x.CommentDate,
            CommentDateHumanized = (-(DateTime.UtcNow - x.CommentDate)).Humanize(),
            Text = x.Text,
            UserImage = x.User.UserImage.Path,
            UserName = x.User.UserName,
            UserId = x.User.Id,
            PostId = x.Post.Id,
            PostText = x.Post.Text.Length > 15 ? x.Post.Text[..15] + "..." : x.Post.Text
        }).ToList();

        return result;
    }
}