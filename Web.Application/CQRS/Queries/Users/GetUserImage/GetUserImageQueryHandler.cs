using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces;

namespace Web.Application.CQRS.Queries.Users.GetUserImage;

public class GetUserImageQueryHandler(IDbContext dbContext) : IRequestHandler<GetUserImageQuery, string>
{
    private readonly IDbContext _dbContext = dbContext;
    public async Task<string> Handle(GetUserImageQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.UserTag))
            return "";

        string userImagePath = await _dbContext.UserImages.Where(x => x.User.UserTag == request.UserTag).Select(x => x.Path).FirstOrDefaultAsync(cancellationToken) ?? "";
        if (string.IsNullOrWhiteSpace(userImagePath))
            return "";

        return userImagePath;
    }
}