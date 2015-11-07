namespace Snippy.App.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Snippy.Data.UnitOfWork;
    using Snippy.Models;

    [Authorize]
    public abstract class BaseController : Controller
    {
        protected BaseController(ISnippyData data)
        {
            this.Data = data;
        }

        protected ISnippyData Data { get; private set; }

        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                this.UserProfile = this.Data.Users
                    .All()
                    .FirstOrDefault(u => u.UserName == requestContext.HttpContext.User.Identity.Name);
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}