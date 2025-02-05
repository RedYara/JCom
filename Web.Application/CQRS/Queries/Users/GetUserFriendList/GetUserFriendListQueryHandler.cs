using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Users.GetUserFriendList;

public class GetUserFriendListQueryHandler(IDbContext dbContext) : IRequestHandler<GetUserFriendListQuery, List<Friend>>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<List<Friend>> Handle(GetUserFriendListQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
            return null;

        return [.. user.Friends];
    }
}