using ChatBotApp.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChatBotTests.Fixtures
{
  public class DatabaseFixture : IDisposable
  {
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public DatabaseFixture()
    {
      _connection = new SqliteConnection("datasource=:memory:");
      _connection.Open();

      _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(_connection).Options;

      using (var context = GetContext())
        context.Database.EnsureCreated();
    }

    public void Dispose()
    {
      _connection.Close();
    }

    public ApplicationDbContext GetContext()
    {
      return new ApplicationDbContext(_options);
    }
  }
}
