using Domain;
using Domain.Enum;
using Web.Application.CQRS.Queries.Users.GetUserComments;
using Web.Application.CQRS.Queries.Users.GetUserFriendList;
using Web.Application.CQRS.Queries.Users.GetUserPosts;

namespace Web.Models.UserDtoModels;

public class ProfileDto
{
    public List<GetUserCommentsVm> UserComments { get; set; } = [];
    public UserFriendListVm UserFriends { get; set; } = new();
    public List<GetUserPostsVm> UserPosts { get; set; } = [];
    public FriendStatus FriendStatus { get; set; }
    public string UserImagePath { get; set; }
    public string UserName { get; set; }
    public string UserTag { get; set; }
}