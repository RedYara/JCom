using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{

    public abstract class BaseController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        //internal Guid UserId => !User.Identity.IsAuthenticated
        //    ? Guid.Empty
        //    : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
