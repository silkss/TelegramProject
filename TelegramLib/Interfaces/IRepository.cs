using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramLib.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task CreateAsync(T entity);
}
