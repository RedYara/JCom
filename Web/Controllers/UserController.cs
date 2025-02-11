using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.CQRS.Queries.Users.GetUserComments;
using Web.Application.CQRS.Queries.Users.GetUserFriendList;
using Web.Application.CQRS.Queries.Users.GetUserFriendStatus;
using Web.Application.CQRS.Queries.Users.GetUserImage;
using Web.Application.CQRS.Queries.Users.GetUserPosts;
using Web.Extensions;
using Web.Models.UserDtoModels;

namespace Web.Controllers;

[Authorize]
public class UserController : BaseController
{
    [Route("[action]")]
    public async Task<IActionResult> Profile(string? id)
    {
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        }
        ProfileDto vm = new();
        string currentUserId = User.Identity.GetUserId();
        if (string.IsNullOrWhiteSpace(id))
            id = User.Identity.GetUserTag();

        if (currentUserId != id)
        {
            var getFriendsStatusQuery = new GetUserFriendStatusQuery()
            {
                CheckingUserTag = id,
                CurrentUserTag = User.Identity.GetUserTag()
            };
            vm.FriendStatus = await Mediator.Send(getFriendsStatusQuery);
        }


        var getFriendsQuery = new GetUserFriendListQuery()
        {
            UserTag = id
        };
        vm.UserFriends = await Mediator.Send(getFriendsQuery);

        var getCommentsQuery = new GetUserCommentsQuery()
        {
            UserTag = id
        };
        vm.UserComments = await Mediator.Send(getCommentsQuery);

        var getUserImageQuery = new GetUserImageQuery()
        {
            UserTag = id
        };
        vm.UserImagePath = await Mediator.Send(getUserImageQuery);

        var getUserPostsQuery = new GetUserPostsQuery()
        {
            UserTag = id,
            CurrentUserId = currentUserId,
            Page = 0
        };
        vm.UserPosts = await Mediator.Send(getUserPostsQuery);

        vm.UserName = User.Identity.GetUserName();
        vm.UserTag = id;

        return View(vm);
    }
}