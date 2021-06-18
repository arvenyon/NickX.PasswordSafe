using NickX.PasswordSafe.Models.Main;
using NickX.PasswordSafe.WebAPI.Domain.Communication;
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
