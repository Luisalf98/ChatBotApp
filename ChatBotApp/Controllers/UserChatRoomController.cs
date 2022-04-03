using ChatBotApp.Models;
using ChatBotApp.Services;
using ChatBotApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace ChatBotApp.Controllers
{
  [Authorize]
  public class UserChatRoomController : BaseController
  {
    private readonly UserChatRoomService userChatRoomService;
    public UserChatRoomController(UserChatRoomService userChatRoomService)
    {
      this.userChatRoomService = userChatRoomService;
    }

    [HttpPost]
    public IActionResult Create(UserChatRoomCreateModel model)
    {
      try
      {
        if (!ModelState.IsValid)
          return View(model);

        model.UserId = User.GetId();
        userChatRoomService.Create(model);        
        TempData["Success"] = "User successfully joined chat room";

        return RedirectToAction("Show", "ChatRoom", new { id = model.ChatRoomId });
      }
      catch(Exception ex)
      {
        return Error(ex.Message);
      }
    }
  }
}
