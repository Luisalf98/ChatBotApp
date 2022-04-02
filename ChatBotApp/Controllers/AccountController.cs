using ChatBotApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatBotApp.Controllers
{
  public class AccountController : Controller
  {
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserModel model)
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

      return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      return RedirectToAction("Login");
    }
  }
}
