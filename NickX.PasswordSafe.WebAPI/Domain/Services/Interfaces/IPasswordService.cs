using NickX.PasswordSafe.WebAPI.Domain.Communication;
using NickX.PasswordSafe.WebAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NickX.PasswordSafe.WebAPI.Domain.Services.Interfaces
{
    public interface IPasswordService
    {
        Task<IEnumerable<Password>> ListAsync();
        Task<PasswordResponse> SaveAsync(Password password);
        Task<PasswordResponse> UpdateAsync(int id, Password password);
        Task<PasswordResponse> DeleteAsync(int id);
    }
}
