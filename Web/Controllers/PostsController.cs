using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class PostsController : BaseController
{
    [HttpPost]
    public IActionResult PublishPost()
    {
        return Ok();
    }
}