using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Users.GetUserFriendList;

public class GetUserFriendListQueryHandler(IDbContext dbContext) : IRequestHandler<GetUserFriendListQuery, UserFriendListVm>
{
    private readonly IDbContext _dbContext = dbContext;

    public async Task<UserFriendListVm> Handle(GetUserFriendListQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(x => x.UserTag == request.UserTag)
            .Include(u => u.Friends)
            .Include(x => x.UserImage)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            return new UserFriendListVm();

        var friends = user.Friends
            .Where(f => _dbContext.Users
            .Include(x => x.Friends)
            .FirstOrDefault(u => u.Id == f.FriendId)?
            .Friends.Any(friend => friend.FriendId == user.Id) ?? false)
            .Select(f =>
            {
                var friendEntity = _dbContext.Users
                    .Include(x => x.UserImage)
                    .Include(x => x.Friends)
                    .FirstOrDefault(u => u.Id == f.FriendId);

                return new UserFriendVm
                {
                    Id = f.FriendId,
                    UserName = friendEntity?.UserName,
                    UserImagePath = friendEntity?.UserImage?.Path,
                    UserTag = friendEntity?.UserTag
                };
            }).ToList();

        return new UserFriendListVm { Friends = friends };
    }
}