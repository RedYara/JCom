using Domain.Enum;
using MediatR;

namespace Web.Application.CQRS.Commands.Friends.RemoveUserFromFriendList;
public class RemoveUserFromFriendListCommand : IRequest<FriendStatus>
{
    public string CurrentUserTag { get; set; }
    public string FriendUserTag { get; set; }
}