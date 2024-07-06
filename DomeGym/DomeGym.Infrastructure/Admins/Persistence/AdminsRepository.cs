using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Admins;
using DomeGym.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Admins.Persistence;

public class AdminsRepository(GymManagementDbContext _dbContext) : IAdminsRepository
{
    public async Task AddAdminAsync(Admin admin)
    {
        await _dbContext.Admins.AddAsync(admin);
    }

    public async Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return await _dbContext.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == adminId);
    }

    public Task UpdateAsync(Admin admin)
    {
        _dbContext.Admins.Update(admin);

        return Task.CompletedTask;
    }
}