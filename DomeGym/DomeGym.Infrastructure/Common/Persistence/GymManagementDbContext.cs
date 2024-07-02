using System.Reflection;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Common.Persistence;

public class GymManagementDbContext : DbContext, IUnitOfWork
{
    public GymManagementDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}