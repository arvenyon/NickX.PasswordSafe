using NickX.PasswordSafe.Models.Main;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NickX.PasswordSafe.WebAPI.Domain.Repositories
{
    public interface IPasswordRepository
    {
        Task<IEnumerable<Password>> ListAsync();
        Task AddAsync(Password password);
        Task<Password> FindByIdAsync(int id);
        void Update(Password password);
        void Remove(Password password);
    }
}
