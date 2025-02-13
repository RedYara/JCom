using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Commands.Friends.RemoveUserFromFriendList;
public class RemoveUserFromFriendListCommandHandler(IDbContext dbContext) : IRequestHandler<RemoveUserFromFriendListCommand, FriendStatus>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<FriendStatus> Handle(RemoveUserFromFriendListCommand request, CancellationToken cancellationToken)
    {
        var currentUserEntity = await _dbContext.Users.Include(x => x.Friends).FirstOrDefaultAsync(x => x.UserTag == request.CurrentUserTag, cancellationToken);
        var friendUserEntity = await _dbContext.Users.Include(x => x.Friends).FirstOrDefaultAsync(x => x.UserTag == request.FriendUserTag, cancellationToken);

        // TODO: Доработать если друг уже удалён из списка друзей
        if (currentUserEntity is null || friendUserEntity is null)
            return FriendStatus.NotFriends;


        if (currentUserEntity.Friends.Any(x => x.FriendId == friendUserEntity.Id))
        {
            var friendEntity = currentUserEntity.Friends.FirstOrDefault(x => x.FriendId == friendUserEntity.Id);
            currentUserEntity.Friends.Remove(friendEntity);

            await _dbContext.SaveChangesAsync(cancellationToken);


            // TODO: Доработать если пользователь находится у кого-то в списке друзей
            if (friendUserEntity.Friends.Any(x => x.FriendId == currentUserEntity.Id))
                return FriendStatus.NotFriends;

            return FriendStatus.NotFriends;
        }

        return FriendStatus.NotFriends;
    }
}