using MediatR;

namespace Web.Application.CQRS.Commands.Comments.LeaveComment;

public class LeaveCommentCommand : IRequest<bool>
{
    public string UserTag { get; set; }
    public string Text { get; set; }
    public int PostId { get; set; }
}