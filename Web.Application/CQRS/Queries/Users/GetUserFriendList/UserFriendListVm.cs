namespace Web.Application.CQRS.Queries.Users.GetUserFriendList;

public class UserFriendVm
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string UserTag { get; set; }
    public string UserImagePath { get; set; }
}

public class UserFriendListVm
{
    public List<UserFriendVm> Friends { get; set; } = new();
}