using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Commands.Friends.AddUserToFriendList;

public class AddUserToFriendListCommandHandler(IDbContext dbContext) : IRequestHandler<AddUserToFriendListCommand, FriendStatus>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<FriendStatus> Handle(AddUserToFriendListCommand request, CancellationToken cancellationToken)
    {
        var currentUserEntity = await _dbContext.Users.Include(x => x.Friends).FirstOrDefaultAsync(x => x.UserTag == request.CurrentUserTag, cancellationToken);
        var friendUserEntity = await _dbContext.Users.Include(x => x.Friends).FirstOrDefaultAsync(x => x.UserTag == request.FriendUserTag, cancellationToken);

        if (currentUserEntity is null || friendUserEntity is null)
            return FriendStatus.NotFriends;


        if (!currentUserEntity.Friends.Any(x => x.User == currentUserEntity && x.FriendId == friendUserEntity.Id))
        {
            currentUserEntity.Friends.Add(new Domain.Friend
            {
                User = currentUserEntity,
                FriendId = friendUserEntity.Id
            });
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (friendUserEntity.Friends.Any(x => x.User == friendUserEntity && x.FriendId == currentUserEntity.Id))
                return FriendStatus.Friends;

            return FriendStatus.Subscribed;
        }

        return FriendStatus.NotFriends;
    }
}