using ChatBotApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatBotApp.Controllers
{
  public class AccountController : BaseController
  {
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginModel model)
    {
      try
      {
        if (!ModelState.IsValid)
          return View(model);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, model.User.Username));
        var claimsIdentity = new ClaimsIdentity(
          claims, CookieAuthenticationDefaults.AuthenticationScheme
        );
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(claimsPrincipal);

        return Redirect("/");
      }
      catch (Exception ex)
      {
        return Error(ex.Message);
      }
    }

    public async Task<IActionResult> Logout()
    {
      try
      {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login");
      }
      catch(Exception ex)
      {
        return Error(ex.Message);
      }
    }
  }
}
