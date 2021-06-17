using System.Threading.Tasks;

namespace NickX.PasswordSafe.WebAPI.Domain.UnitOfWorkManagement
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync();
    }
}
