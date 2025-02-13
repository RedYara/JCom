using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Users.GetUserSubscriptionList;

public class GetUserSubscriptionListQueryHandler(IDbContext dbContext) : IRequestHandler<GetUserSubscriptionListQuery, UserSubscriptionListVm>
{
    private readonly IDbContext _dbContext = dbContext;

    public async Task<UserSubscriptionListVm> Handle(GetUserSubscriptionListQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(x => x.UserTag == request.UserTag)
            .Include(u => u.Friends)
            .Include(x => x.UserImage)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            return new UserSubscriptionListVm();

        var subscriptions = user.Friends
            .Where(f => !_dbContext.Users
            .Include(x => x.Friends)
            .FirstOrDefault(u => u.Id == f.FriendId)?
            .Friends.Any(friend => friend.FriendId == user.Id) ?? false)
            .Select(f =>
            {
                var friendEntity = _dbContext.Users
                    .Include(x => x.UserImage)
                    .Include(x => x.Friends)
                    .FirstOrDefault(u => u.Id == f.FriendId);

                return new UserSubscriptionVm
                {
                    Id = f.FriendId,
                    UserName = friendEntity?.UserName,
                    UserImagePath = friendEntity?.UserImage?.Path,
                    UserTag = friendEntity?.UserTag
                };
            }).ToList();

        return new UserSubscriptionListVm { Subscriptions = subscriptions };
    }
}