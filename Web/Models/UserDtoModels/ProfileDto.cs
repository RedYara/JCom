using Domain;
using Web.Application.CQRS.Queries.Users.GetUserComments;
using Web.Application.CQRS.Queries.Users.GetUserPosts;

namespace Web.Models.UserDtoModels;

public class ProfileDto
{
    public List<GetUserCommentsVm> UserComments { get; set; }
    public List<Friend> UserFriends { get; set; }
    public List<GetUserPostsVm> UserPosts { get; set; }
    public string UserImagePath { get; set; }
    public string UserName { get; set; }
    public string UserTag { get; set; }
}