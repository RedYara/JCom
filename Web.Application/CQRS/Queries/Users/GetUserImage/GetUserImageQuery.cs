using MediatR;

namespace Web.Application.CQRS.Queries.Users.GetUserImage;

public class GetUserImageQuery : IRequest<string>
{
    public string UserId { get; set; } = "";
}