using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Subscriptions;
using DomeGym.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Subscriptions.Persistence;

public class SubscriptionsRepository(GymManagementDbContext dbContext) : ISubscriptionsRepository
{
    private readonly GymManagementDbContext _dbContext = dbContext;

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await _dbContext.Subscriptions.AddAsync(subscription);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Subscriptions
            .AsNoTracking()
            .AnyAsync(subscription => subscription.Id == id);
    }

    public async Task<Subscription?> GetByAdminIdAsync(Guid adminId)
    {
        return await _dbContext.Subscriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(subscription => subscription.AdminId == adminId);
    }

    public async Task<Subscription?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Subscriptions.FirstOrDefaultAsync(subscription => subscription.Id == id);
    }

    public async Task<List<Subscription>> ListAsync()
    {
        return await _dbContext.Subscriptions.ToListAsync();
    }

    public Task RemoveSubscriptionAsync(Subscription subscription)
    {
        _dbContext.Remove(subscription);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Subscription subscription)
    {
        _dbContext.Update(subscription);

        return Task.CompletedTask;
    }
}