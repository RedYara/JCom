using Domain;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Users.GetUserFriendStatus;

public class GetUserFriendStatusQueryHandler(IDbContext dbContext) : IRequestHandler<GetUserFriendStatusQuery, FriendStatus>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<FriendStatus> Handle(GetUserFriendStatusQuery request, CancellationToken cancellationToken)
    {
        FriendStatus result = FriendStatus.NotFriends;
        var currentUserEntity = await _dbContext.Users.Include(x => x.Friends).FirstOrDefaultAsync(x => x.UserTag == request.CurrentUserTag, cancellationToken);
        var checkingUserEntity = await _dbContext.Users.Include(x => x.Friends).FirstOrDefaultAsync(x => x.UserTag == request.CheckingUserTag, cancellationToken);

        if (currentUserEntity is null || checkingUserEntity is null)
            return result;

        if (currentUserEntity.Friends.Any(x => x.User == currentUserEntity && x.FriendId == checkingUserEntity.Id))
        {
            result = FriendStatus.Subscribed;
            if (checkingUserEntity.Friends.Any(x => x.User == checkingUserEntity && x.FriendId == currentUserEntity.Id))
                result = FriendStatus.Friends;
        }

        return result;
    }
}