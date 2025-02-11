using MediatR;

namespace Web.Application.CQRS.Queries.Users.GetUserPosts;

public class GetUserPostsQuery : IRequest<List<GetUserPostsVm>>
{
    public int Page { get; set; }
    public string UserTag { get; set; }
    public string CurrentUserId { get; set; }
}