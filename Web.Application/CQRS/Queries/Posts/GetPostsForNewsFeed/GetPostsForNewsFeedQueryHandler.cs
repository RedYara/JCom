using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Posts.GetPostsForNewsFeed;

public class GetPostsForNewsFeedQueryHandler(IDbContext dbContext) : IRequestHandler<GetPostsForNewsFeedQuery, List<Post>>
{
    private IDbContext _dbContext = dbContext;
    // TODO: Допилить метод для вывода постов друзей
    public async Task<List<Post>> Handle(GetPostsForNewsFeedQuery query, CancellationToken cancellationToken)
    {
        // Получение последних 5 постов от юзера
        var posts = await _dbContext.Posts.Where(x => x.User == query.User).OrderByDescending(x => x.Id).Take((query.Page + 1) * 5).ToListAsync();

        return posts;
    }
}