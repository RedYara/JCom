using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.CQRS.Queries.Posts.GetPostsForNewsFeed;
using Web.Application.CQRS.Queries.Users.GetUserImage;
using Web.Extensions;
using Web.Models;

namespace Web.Controllers;

[Authorize]
public class HomeController(ILogger<HomeController> logger) : BaseController
{
    public async Task<IActionResult> Index()
    {
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        }
        var userImageQuery = new GetUserImageQuery()
        {
            UserTag = User.Identity.GetUserTag()
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
