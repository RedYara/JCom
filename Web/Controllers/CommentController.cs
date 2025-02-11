using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web.Application.CQRS.Commands.Comments.DeleteComment;
using Web.Application.CQRS.Commands.Comments.LeaveComment;
using Web.Application.CQRS.Queries.Comments.GetPostComments;
using Web.Extensions;
using Web.Models.CommentDtoModels;

namespace Web.Controllers;

public class CommentController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetPostComments(int postId)
    {
        var query = new GetPostCommentsQuery()
        {
            PostId = postId
        };
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] AddCommentDto request)
    {
        var command = new LeaveCommentCommand()
        {
            PostId = request.PostId,
            Text = request.Text,
            UserTag = User.Identity.GetUserTag()
        };
        var vm = await Mediator.Send(command);
        return vm ? Ok() : BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveComment([FromBody] DeleteCommentDto request)
    {
        var command = new DeleteCommentCommand()
        {
            CommentId = request.CommentId,
            UserTag = User.Identity.GetUserTag(),
            PostId = request.PostId
        };
        var vm = await Mediator.Send(command);

        return vm ? Ok() : StatusCode(500);
    }
}