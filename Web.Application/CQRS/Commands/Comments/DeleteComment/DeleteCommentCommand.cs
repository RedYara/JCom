using MediatR;

namespace Web.Application.CQRS.Commands.Comments.DeleteComment;

public class DeleteCommentCommand : IRequest<bool>
{
    public int CommentId { get; set; }
    public string UserId { get; set; }
}