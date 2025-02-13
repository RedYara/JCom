using Domain;
using MediatR;

namespace Web.Application.CQRS.Queries.Users.GetUserFriendList;

public class GetUserFriendListQuery : IRequest<UserFriendListVm>
{
    public string UserTag { get; set; }
}