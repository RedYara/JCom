namespace Web.Application.CQRS.Queries.Users.GetUserSubscriptionList;

public class UserSubscriptionVm
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string UserTag { get; set; }
    public string UserImagePath { get; set; }
}

public class UserSubscriptionListVm
{
    public List<UserSubscriptionVm> Subscriptions { get; set; } = new();
}
