using Domain.Enum;
using MediatR;

namespace Web.Application.CQRS.Commands.Friends.AddUserToFriendList;

public class AddUserToFriendListCommand : IRequest<FriendStatus>
{
    public string CurrentUserTag { get; set; }
    public string FriendUserTag { get; set; }
}