using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.CQRS.Queries.Posts.GetPostsForNewsFeed;
using Web.Application.CQRS.Queries.Users.GetUserImage;
using Web.Models;

namespace Web.Controllers;

[Authorize]
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var userImageQuery = new GetUserImageQuery()
        {
            UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        };
        var userImageQueryResult = await Mediator.Send(userImageQuery);


        var vm = new NewsFeedDto()
        {
            ImagePath = userImageQueryResult
        };

        return View(vm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

}
