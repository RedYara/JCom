using MediatR;

namespace Web.Application.CQRS.Commands.Comments.DeleteComment;

public class DeleteCommentCommand : IRequest<bool>
{
    public int CommentId { get; set; }
    public string UserTag { get; set; }
    public int PostId { get; set; }
}