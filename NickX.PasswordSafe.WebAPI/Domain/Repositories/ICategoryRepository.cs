using NickX.PasswordSafe.Models.Main;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NickX.PasswordSafe.WebAPI.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task AddAsync(Category category);
        Task<Category> FindByIdAsync(int id);
        void Update(Category category);
        void Remove(Category category);
    }
}
