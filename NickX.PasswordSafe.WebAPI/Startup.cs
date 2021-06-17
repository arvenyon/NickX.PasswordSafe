using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using NickX.PasswordSafe.WebAPI.Domain.Repositories;
using NickX.PasswordSafe.WebAPI.Domain.Services.Classes;
using NickX.PasswordSafe.WebAPI.Domain.Services.Interfaces;
using NickX.PasswordSafe.WebAPI.Domain.UnitOfWorkManagement;
using NickX.PasswordSafe.WebAPI.Persistence.Contexts;
using NickX.PasswordSafe.WebAPI.Persistence.Repositories;
using NickX.PasswordSafe.WebAPI.Persistence.UnitOfWorkManagement;

namespace NickX.PasswordSafe.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);

            var conString = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\NickX.PasswordSafe", "SqlConnectionString", null).ToString();
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(conString));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPasswordService, PasswordService>();

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
