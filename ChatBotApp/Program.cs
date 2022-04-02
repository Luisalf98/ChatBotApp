using ChatBotApp.Data;
using ChatBotApp.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ChatBotApp
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      CreateDatabaseIfNotExists(host);
      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                   webBuilder.UseStartup<Startup>();
                 });
    }

    public static void CreateDatabaseIfNotExists(IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        try
        {
          var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
          DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
    }
  }
}
