using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Web.Application.CQRS.Commands.Likes.LikePost;
using Web.Models.LikeDtoModels;

namespace Web.Controllers;

public class LikeController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> LikePost([FromBody] LikePostDto request)
    {
        var command = new LikePostCommand()
        {
            PostId = request.PostId,
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        };
        var commandResult = await Mediator.Send(command);
        return commandResult ? Ok() : BadRequest();
    }
}