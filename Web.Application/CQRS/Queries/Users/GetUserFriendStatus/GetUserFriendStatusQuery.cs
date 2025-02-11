using Domain.Enum;
using MediatR;

namespace Web.Application.CQRS.Queries.Users.GetUserFriendStatus;

public class GetUserFriendStatusQuery : IRequest<FriendStatus>
{
    public string CurrentUserTag { get; set; }
    public string CheckingUserTag { get; set; }
}