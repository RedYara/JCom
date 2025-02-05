using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.CQRS.Commands.Posts.PublishPost;
using Web.Application.CQRS.Queries.Posts.GetPostsForNewsFeed;
using Web.Models.PostDtoModels;

namespace Web.Controllers;

[Authorize]
public class PostsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> PublishPost([FromBody] PublishPostDto request)
    {
        var command = new PublishPostCommand()
        {
            Text = request.Text,
            UserId = request.UserId
        };
        int postId = await Mediator.Send(command);
        return Ok(postId);
    }

    [HttpGet]
    public async Task<IActionResult> LoadPosts(string userId, int page)
    {
        var postsQuery = new GetPostsForNewsFeedQuery()
        {
            Page = page,
            UserId = userId
        };
        var vm = await Mediator.Send(postsQuery);
        return Ok(vm);
    }
}