using MediatR;

namespace Web.Application.CQRS.Queries.Users.GetUserSubscriptionList;

public class GetUserSubscriptionListQuery : IRequest<UserSubscriptionListVm>
{
    public string UserTag { get; set; }
}
