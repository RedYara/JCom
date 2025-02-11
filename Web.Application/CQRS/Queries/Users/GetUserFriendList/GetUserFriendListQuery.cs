using Domain;
using MediatR;

namespace Web.Application.CQRS.Queries.Users.GetUserFriendList;

public class GetUserFriendListQuery : IRequest<List<Friend>>
{
    public string UserTag { get; set; }
}