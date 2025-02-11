using MediatR;

namespace Web.Application.CQRS.Queries.Users.GetUserImage;

public class GetUserImageQuery : IRequest<string>
{
    public string UserTag { get; set; } = "";
}