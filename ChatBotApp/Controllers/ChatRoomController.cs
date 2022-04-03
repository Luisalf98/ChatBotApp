using ChatBotApp.Models;
using ChatBotApp.Services;
using ChatBotApp.Utilities;
using ChatBotApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBotApp.Controllers
{
  [Authorize]
  public class ChatRoomController : BaseController
  {
    private readonly ChatRoomService chatRoomService;
    private readonly ChatService chatService;
    private readonly IAuthorizationService authorizationService;
    public ChatRoomController(
      ChatRoomService chatRoomService, 
      IAuthorizationService authorizationService,
      ChatService chatService
    )
    {
      this.chatRoomService = chatRoomService;
      this.authorizationService = authorizationService;
      this.chatService = chatService;
    }

    public IActionResult Index()
    {
      try
      {
        var model = new ChatRoomIndexViewModel
        {
          OtherChats = chatRoomService.GetOthers(User.GetId()),
          UserChats = chatRoomService.GetAllByUserId(User.GetId())
        };
        return View(model);
      }
      catch (Exception ex)
      {
        return Error(ex.Message);
      }
    }

    public async Task<IActionResult> Show(long id, int limit = 50)
    {
      try
      {
        var authorizationResult =
          await authorizationService.AuthorizeAsync(User, id, "ChatRoomPolicy");
        if (!authorizationResult.Succeeded)
          return Forbid();

        var model = chatRoomService.GetById(id);
        if (model == null)
          return NotFound();

        model.Messages = chatService.GetAllByChatRoomId(id)
                                    .Take(50)
                                    .Reverse()
                                    .ToList();
        return View(model);
      }
      catch (Exception ex)
      {
        return Error(ex.Message);
      }
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(ChatRoomCreateModel model)
    {
      try
      {
        model.UserId = User.GetId();
        if (!ModelState.IsValid || chatRoomService.Create(model) == null)
          return View(model);
        
        TempData["Success"] = "Chat Room successfully created";

        return Redirect("/");
      }
      catch(Exception ex)
      {
        return Error(ex.Message);
      }
    }
  }
}
