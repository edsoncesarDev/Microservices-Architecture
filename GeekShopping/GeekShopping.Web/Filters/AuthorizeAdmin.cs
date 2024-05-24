using GeekShopping.Web.Models;
using GeekShopping.Web.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace GeekShopping.Web.Filters;

public sealed class AuthorizeAdmin : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string sessionUser = context.HttpContext.Session.GetString("LoggedUser")!;

        if (string.IsNullOrEmpty(sessionUser))
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
        }
        else
        {
            UserModel usuario = JsonSerializer.Deserialize<UserModel>(sessionUser)!;

            if (usuario == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }

            if(usuario!.Role != AuthorizeRole.Admin)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
            }
        }

        base.OnActionExecuting(context);
    }
}
