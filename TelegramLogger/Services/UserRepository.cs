using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramLib.Interfaces;
using TelegramLib.Models;

namespace TelegramProject.Services;

internal class UserRepository : IRepository<UserModel>
{
    private readonly IDbContextFactory<DataContext> _dbContextFactory;

    public UserRepository(IDbContextFactory<DataContext> dbContextFactory )
    {
        _dbContextFactory = dbContextFactory;
    }
    public async Task CreateAsync(UserModel entity)
    {
        if (entity == null) return;
        using (var dbcontext = _dbContextFactory.CreateDbContext())
        {
            dbcontext.Set<UserModel>().Add(entity);
            await dbcontext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        IEnumerable<UserModel> temp;
        using (var datacontext = _dbContextFactory.CreateDbContext())
        {
            temp = await datacontext.Set<UserModel>().ToListAsync();
        }
        return temp;
    }
}
