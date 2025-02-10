using MediatR;

namespace Web.Application.CQRS.Commands.Likes.LikePost;

public class LikePostCommand : IRequest<bool>
{
    public int PostId { get; set; }
    public string UserId { get; set; }
}