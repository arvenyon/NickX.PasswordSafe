using NickX.PasswordSafe.WebAPI.Persistence.Contexts;

namespace NickX.PasswordSafe.WebAPI.Persistence.Repositories
{
    public abstract class BaseRepository
    {

        protected readonly SqlDbContext _context;

        public BaseRepository(SqlDbContext context)
        {
            _context = context;
        }
    }
}
