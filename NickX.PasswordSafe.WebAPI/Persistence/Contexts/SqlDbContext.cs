using Microsoft.EntityFrameworkCore;
using NickX.PasswordSafe.WebAPI.Domain.Models;

namespace NickX.PasswordSafe.WebAPI.Persistence.Contexts
{
    public class SqlDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Password> Passwords { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().ToTable("tbl_categories");
            builder.Entity<Category>().HasKey(c => c.Id);
            builder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Category>().HasMany(c => c.Passwords).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

            builder.Entity<Password>().ToTable("tbl_passwords");
            builder.Entity<Password>().HasKey(k => k.Id);
            builder.Entity<Password>().Property(k => k.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Password>().Property(k => k.DateCreate).IsRequired().HasDefaultValueSql("getdate()");
            builder.Entity<Password>().Property(k => k.DisplayName).IsRequired();
        }

    }
}
