using Domain;
using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Users.GetUserPosts;

public class GetUserPostsQueryHandler(IDbContext dbContext) : IRequestHandler<GetUserPostsQuery, List<GetUserPostsVm>>
{
    private IDbContext _dbContext = dbContext;
    // TODO: Допилить метод для вывода постов друзей
    public async Task<List<GetUserPostsVm>> Handle(GetUserPostsQuery query, CancellationToken cancellationToken)
    {
        // Получение последних 5 постов от юзера
        var posts = await _dbContext.Posts
            .Include(x => x.User)
                .ThenInclude(x => x.UserImage)
            .Where(x => x.User.UserTag == query.UserTag)
            .OrderByDescending(x => x.Id)
            .Take(3)
            .ToListAsync(cancellationToken); // Получаем список постов асинхронно

        // Преобразуем посты в нужный формат
        var result = posts.Select(x => new GetUserPostsVm()
        {
            Id = x.Id,
            LikesCount = _dbContext.Likes.Include(x => x.Post).Count(y => y.Post == x),
            CommentsCount = _dbContext.Comments.Count(y => y.Post.Id == x.Id),
            PostDate = x.PostDate,
            PostDateHumanized = (-(DateTime.UtcNow - x.PostDate)).Humanize(),
            Text = x.Text,
            UserImage = x.User.UserImage.Path,
            UserName = x.User.UserName,
            IsLiked = _dbContext.Likes.Include(x => x.Post).Include(x => x.User).Any(y => y.Post == x && y.User.Id == query.CurrentUserId),
            UserId = x.User.Id
        }).ToList();

        return result;
    }
}