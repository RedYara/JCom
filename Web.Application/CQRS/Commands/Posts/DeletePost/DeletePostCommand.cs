using Domain;
using MediatR;

namespace Web.Application.CQRS.Commands.Posts.DeletePost;

public class DeletePostCommand : IRequest<bool>
{
    public int PostId { get; set; }
    public string UserId { get; set; }
}