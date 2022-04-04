using ChatBotApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatBotApp.Controllers
{
  public abstract class BaseController : Controller
  {
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    protected IActionResult Error(string errorMessage)
    {
      TempData["Error"] = errorMessage;
      return View(
        "~/Views/Shared/Error.cshtml",
        new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
      );
    }
  }
}
