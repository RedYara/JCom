using Domain;
using MediatR;

namespace Web.Application.CQRS.Queries.Users.GetUserComments;

public class GetUserCommentsQuery : IRequest<List<GetUserCommentsVm>>
{
    public int Page { get; set; }
    public string UserId { get; set; }
}