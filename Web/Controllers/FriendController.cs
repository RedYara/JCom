using System.Threading.Tasks;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.CQRS.Commands.Friends.AddUserToFriendList;
using Web.Application.CQRS.Commands.Friends.RemoveUserFromFriendList;
using Web.Application.CQRS.Queries.Users.GetUserFriendList;
using Web.Application.CQRS.Queries.Users.GetUserSubscriptionList;
using Web.Extensions;
using Web.Models.FriendDtoModels;

namespace Web.Controllers;

[Authorize]
public class FriendController : BaseController
{
    [Route("Friends")]
    public async Task<IActionResult> FriendList(string? userTag)
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetFriendList(string? userTag)
    {
        userTag ??= User.Identity.GetUserTag();
        var query = new GetUserFriendListQuery()
        {
            UserTag = userTag
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpGet]
    public async Task<IActionResult> GetSubscriptions(string? userTag)
    {
        userTag ??= User.Identity.GetUserTag();
        var query = new GetUserSubscriptionListQuery()
        {
            UserTag = userTag
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpPost]
    public async Task<FriendStatus> AddToFriends([FromBody] AddToFriendsDto request)
    {
        var command = new AddUserToFriendListCommand()
        {
            CurrentUserTag = User.Identity.GetUserTag(),
            FriendUserTag = request.UserTag
        };
        var result = await Mediator.Send(command);
        return result;
    }

    [HttpPost]
    public async Task<FriendStatus> RemoveFriend([FromBody] AddToFriendsDto request)
    {
        var command = new RemoveUserFromFriendListCommand()
        {
            CurrentUserTag = User.Identity.GetUserTag(),
            FriendUserTag = request.UserTag
        };
        var result = await Mediator.Send(command);
        return result;
    }
}