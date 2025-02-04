using Domain;
using MediatR;

namespace Web.Application.CQRS.Queries.Posts.GetPostsForNewsFeed;

public class GetPostsForNewsFeedQuery : IRequest<List<Post>>
{
    public int Page { get; set; }
    public User User { get; set; }
}