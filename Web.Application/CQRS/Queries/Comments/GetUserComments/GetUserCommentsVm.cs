using Domain;
using MediatR;

namespace Web.Application.CQRS.Queries.Comments.GetUserComments;

public class GetUserCommentsVm : IRequest<List<Comment>>
{
    public string UserName { get; set; }
    public string UserImagePath { get; set; }
    public string Text { get; set; }
    public string CommentDate { get; set; }
    public string CommentDateHumanized { get; set; }
}