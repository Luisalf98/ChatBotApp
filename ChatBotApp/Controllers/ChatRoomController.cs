using ChatBotApp.Models;
using ChatBotApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChatBotApp.Controllers
{
  [Authorize]
  public class ChatRoomController : BaseController
  {
    private readonly ChatRoomService chatRoomService;
    public ChatRoomController(ChatRoomService chatRoomService)
    {
      this.chatRoomService = chatRoomService;
    }

    public IActionResult Index()
    {
      try
      {
        var model = chatRoomService.GetAll();
        return View(model);
      }
      catch (Exception ex)
      {
        return Error(ex.Message);
      }
    }

    public IActionResult Show(long id)
    {
      try
      {
        var model = chatRoomService.GetById(id);
        if (model == null)
          return NotFound();

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
