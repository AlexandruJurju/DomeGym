using DomeGym.Infrastructure.Common.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Application.SubcutaneousTests.Common;

/// <summary>
///     In Subcutaneous tests we aren't testing integration with a real database,
///     so even if we weren't using SQLite we would use some in-memory database.
/// </summary>
public class SqliteTestDatabase : IDisposable
{
    private SqliteTestDatabase(string connectionString)
    {
        Connection = new SqliteConnection(connectionString);
    }

    public SqliteConnection Connection { get; }

    public void Dispose()
    {
        Connection.Close();
    }

    public static SqliteTestDatabase CreateAndInitialize()
    {
        var testDatabase = new SqliteTestDatabase("DataSource=:memory:");

        testDatabase.InitializeDatabase();

        return testDatabase;
    }

    public void InitializeDatabase()
    {
        Connection.Open();
        var options = new DbContextOptionsBuilder<GymManagementDbContext>()
            .UseSqlite(Connection)
            .Options;

        var context = new GymManagementDbContext(options, null!, null!);

        context.Database.EnsureCreated();
    }

    public void ResetDatabase()
    {
        Connection.Close();

        InitializeDatabase();
    }
}