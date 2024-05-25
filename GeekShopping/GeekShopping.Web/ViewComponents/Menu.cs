
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.ViewComponents;

public class Menu : ViewComponent
{
    private readonly ISessionUser _session;

    public Menu(ISessionUser session)
    {
        _session = session;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = _session.GetUserSession();

        if(user is null)
        {
            return View(new UserModel());
        }

        return View(user);
    }
}
