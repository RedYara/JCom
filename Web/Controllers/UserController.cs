using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.CQRS.Queries.Users.GetUserComments;
using Web.Application.CQRS.Queries.Users.GetUserFriendList;
using Web.Application.CQRS.Queries.Users.GetUserImage;
using Web.Application.CQRS.Queries.Users.GetUserPosts;
using Web.Models.UserDtoModels;

namespace Web.Controllers;

[Authorize]
public class UserController : BaseController
{
    [Route("[action]")]
    public async Task<IActionResult> Profile(string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var getFriendsQuery = new GetUserFriendListQuery()
        {
            UserId = userId
        };
        var getFriendsResult = await Mediator.Send(getFriendsQuery);

        var getCommentsQuery = new GetUserCommentsQuery()
        {
            UserId = userId
        };
        var getCommentsResult = await Mediator.Send(getCommentsQuery);

        var getUserImageQuery = new GetUserImageQuery()
        {
            UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        };
        var getUserImageResult = await Mediator.Send(getUserImageQuery);

        var getUserPostsQuery = new GetUserPostsQuery()
        {
            UserId = userId,
            Page = 0
        };
        var getUserPostsResult = await Mediator.Send(getUserPostsQuery);

        var vm = new ProfileDto()
        {
            UserComments = getCommentsResult,
            UserFriends = getFriendsResult,
            UserPosts = getUserPostsResult,
            UserImagePath = getUserImageResult,
            UserName = User.Identity.Name,
            UserTag = User.Identity.Name
        };

        return View(vm);
    }
}