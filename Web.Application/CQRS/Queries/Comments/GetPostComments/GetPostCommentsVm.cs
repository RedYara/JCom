using Domain;
using MediatR;

namespace Web.Application.CQRS.Queries.Comments.GetPostComments;

public class GetPostCommentsVm : IRequest<List<Comment>>
{
    public int CommentId { get; set; }
    public int PostId { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
    public string UserImagePath { get; set; }
    public string Text { get; set; }
    public string CommentDate { get; set; }
    public string CommentDateHumanized { get; set; }
}