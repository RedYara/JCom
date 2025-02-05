using Domain;
using MediatR;

namespace Web.Application.CQRS.Commands.Posts.PublishPost;

public class PublishPostCommand : IRequest<int>
{
    public string Text { get; set; }
    public string UserId { get; set; }
}