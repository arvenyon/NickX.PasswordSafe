using Microsoft.EntityFrameworkCore;
using NickX.PasswordSafe.Models.Main;
using NickX.PasswordSafe.WebAPI.Domain.Repositories;
using NickX.PasswordSafe.WebAPI.Persistence.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NickX.PasswordSafe.WebAPI.Persistence.Repositories
{
    public class PasswordRepository : BaseRepository, IPasswordRepository
    {
        public PasswordRepository(SqlDbContext context) : base(context) { }

        public async Task AddAsync(Password password)
        {
            await _context.Passwords.AddAsync(password);
        }

        public async Task<Password> FindByIdAsync(int id)
        {
            return await _context.Passwords.FindAsync(id);
        }

        public async Task<IEnumerable<Password>> ListAsync()
        {
            return await _context.Passwords.ToListAsync();
        }

        public void Remove(Password password)
        {
            _context.Passwords.Remove(password);
        }

        public void Update(Password password)
        {
            _context.Passwords.Update(password);
        }
    }
}
