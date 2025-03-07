using Domain;
using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Posts.GetPostsForNewsFeed;

public class GetPostsForNewsFeedQueryHandler(IDbContext dbContext) : IRequestHandler<GetPostsForNewsFeedQuery, List<GetPostsVm>>
{
    private IDbContext _dbContext = dbContext;
    // TODO: Допилить метод для вывода постов друзей
    public async Task<List<GetPostsVm>> Handle(GetPostsForNewsFeedQuery query, CancellationToken cancellationToken)
    {

        var userFrineds = await _dbContext.Users
            .Include(x => x.Friends)
            .FirstOrDefaultAsync(x => x.Id == query.UserId, cancellationToken);

        if (userFrineds == null)
        {
            return [];
        }

        var friendsIds = userFrineds.Friends.Where(f => f.User.Id == query.UserId)
            .Select(f => f.FriendId)
            .ToList();

        // Добавление самого пользователя в список
        friendsIds.Add(query.UserId);

        // Получение последних 5 постов от пользователя и его друзей
        var posts = await _dbContext.Posts
            .Include(x => x.User)
                .ThenInclude(x => x.UserImage)
            .Where(x => friendsIds.Contains(x.User.Id))
            .OrderByDescending(x => x.Id)
            .Skip(query.Page * 5)
            .Take(5)
            .ToListAsync(cancellationToken); // Получаем список постов асинхронно

        // Преобразуем посты в нужный формат
        var result = posts.Select(x => new GetPostsVm()
        {
            PostId = x.Id,
            LikesCount = _dbContext.Likes.Include(x => x.Post).Count(y => y.Post == x),
            PostDate = x.PostDate,
            PostDateHumanized = (-(DateTime.UtcNow - x.PostDate)).Humanize(),
            Text = x.Text,
            UserImage = x.User.UserImage.Path,
            UserName = x.User.UserName,
            UserPostedTag = x.User.UserTag,
            UserId = x.User.Id,
            CommentsCount = _dbContext.Comments.Where(y => y.Post.Id == x.Id).Count(),
            IsLiked = _dbContext.Likes.Include(x => x.Post).Include(x => x.User).Any(y => y.Post == x && y.User.Id == query.UserId)

        }).ToList();

        return result;
    }
}