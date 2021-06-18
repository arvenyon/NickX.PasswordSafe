using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using NickX.PasswordSafe.WebAPI.Domain.Manager.Classes;
using NickX.PasswordSafe.WebAPI.Domain.Manager.Interfaces;
using NickX.PasswordSafe.WebAPI.Domain.Repositories;
using NickX.PasswordSafe.WebAPI.Domain.Services.Classes;
using NickX.PasswordSafe.WebAPI.Domain.Services.Interfaces;
using NickX.PasswordSafe.WebAPI.Domain.UnitOfWorkManagement;
using NickX.PasswordSafe.WebAPI.Persistence.Contexts;
using NickX.PasswordSafe.WebAPI.Persistence.Repositories;
using NickX.PasswordSafe.WebAPI.Persistence.UnitOfWorkManagement;
using System.Text;

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
            // get values from registry
            var privateKey = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\NickX.PasswordSafe", "PrivateKey", null).ToString();
            var conString = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\NickX.PasswordSafe", "SqlConnectionString", null).ToString();

            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);

            // db context
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(conString));

            // repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();

            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPasswordService, PasswordService>();

            // authentication via jwtauth
            services.AddAuthentication
            (
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(
                x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(privateKey));

            // auto mapper
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
