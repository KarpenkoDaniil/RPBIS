using CourceProject.Models;
using DBModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Laba_4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // внедрение зависимости для доступа к БД с использованием EF
            string connection = Configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<MaterialsSuplyContext>(options => options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = false; // требуются ли цифры
            })
               .AddEntityFrameworkStores<MaterialsSuplyContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Loger/GetLoger"; // Новый путь к странице входа
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            // добавление кэширования
            services.AddMemoryCache();

            // добавление поддержки сессии
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // добавляем поддержку статических файлов
            app.UseStaticFiles();

            // добавляем поддержку сессий
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication(); // подключение аутентификации
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Login",
                    pattern: "Login/{controller=Loger}/{action=GetLoger}/{id?}");

                endpoints.MapControllerRoute(
                    name: "LogerRoute",
                    pattern: "Registration/{controller=Loger}/{action=RegistrationMenu}/{id?}");

                endpoints.MapControllerRoute(
                    name: "BasePage",
                    pattern: "/{controller=BasePage}/{action=BasePageView}/{id?}"
                    );
            });
        }
    }
}
