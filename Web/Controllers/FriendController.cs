using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.CQRS.Commands.Friends.AddUserToFriendList;
using Web.Application.CQRS.Commands.Friends.RemoveUserFromFriendList;
using Web.Extensions;
using Web.Models.FriendDtoModels;

namespace Web.Controllers;

[Authorize]
public class FriendController : BaseController
{
    [Route("Friends")]
    public IActionResult FriendList()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetFriendList()
    {
        return Ok();
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