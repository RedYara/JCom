using MediatR;

namespace Web.Application.CQRS.Queries.Comments.GetUserComments;

public class GetUserCommentsQuery : IRequest<List<GetUserCommentsVm>>
{
    public string UserId { get; set; }
}