using ChatBotApp.Data;
using System;

namespace ChatBotApp.Services
{
  public abstract class BaseService : IDisposable
  {
    public ApplicationDbContext context { get; set; }

    public BaseService(ApplicationDbContext context)
    {
      this.context = context;
    }

    public void Dispose()
    {
      context.Dispose();
    }
  }
}
