using Domain;
using MediatR;

namespace Web.Application.CQRS.Queries.Posts.GetPostsForNewsFeed;

public class GetPostsForNewsFeedQuery : IRequest<List<GetPostsVm>>
{
    public int Page { get; set; }
    public string UserId { get; set; }
}