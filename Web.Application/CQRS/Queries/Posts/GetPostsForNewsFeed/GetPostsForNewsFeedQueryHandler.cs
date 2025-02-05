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
        // Получение последних 5 постов от юзера
        var posts = await _dbContext.Posts
            .Include(x => x.User)
                .ThenInclude(x => x.UserImage)
            .Where(x => x.User.Id == query.UserId)
            .OrderByDescending(x => x.Id)
            .Skip(query.Page * 5)
            .Take(5)
            .ToListAsync(cancellationToken); // Получаем список постов асинхронно

        // Преобразуем посты в нужный формат
        var result = posts.Select(x => new GetPostsVm()
        {
            LikesCount = x.Likes,
            PostDate = x.PostDate,
            PostDateHumanized = (-(DateTime.UtcNow - x.PostDate)).Humanize(),
            Text = x.Text,
            UserImage = x.User.UserImage.Path,
            UserName = x.User.UserName,
            UserId = x.User.Id
        }).ToList();

        return result;
    }
}