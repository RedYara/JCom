using Domain;
using MediatR;

namespace Web.Application.CQRS.Queries.Comments.GetPostComments;

public class GetPostCommentsQuery : IRequest<List<GetPostCommentsVm>>
{
    public int PostId { get; set; }
}