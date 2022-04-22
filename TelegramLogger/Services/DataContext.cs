using Microsoft.EntityFrameworkCore;
using TelegramLib.Models;

namespace TelegramProject.Services;

internal class DataContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
}
