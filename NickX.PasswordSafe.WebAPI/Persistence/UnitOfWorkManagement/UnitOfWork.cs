using NickX.PasswordSafe.WebAPI.Domain.UnitOfWorkManagement;
using NickX.PasswordSafe.WebAPI.Persistence.Contexts;
using System.Threading.Tasks;

namespace NickX.PasswordSafe.WebAPI.Persistence.UnitOfWorkManagement
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlDbContext _context;

        public UnitOfWork(SqlDbContext context)
        {
            _context = context;
        }

        public async Task CommitChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
