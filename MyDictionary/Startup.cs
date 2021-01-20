using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyDictionary.Core.Domain;
using MyDictionary.Infrastructure.Interfaces;
using MyDictionary.Infrastructure.Repositories;
using MyDictionary.Infrastructure.Respositories;
using MyDictionary.Infrastructure.Services;

namespace MyDictionary
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
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyHeader());
            });

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "401161494104-ve7ajs942ki0l98eolqm3n3ranm7ro6f.apps.googleusercontent.com";
                    options.ClientSecret = "_Z11GsRMjSNW4uKGt_6tHKKW";
                });


            services.AddSingleton<IWordLookupRepository>(s => new RapidApiRepository(
                Configuration.GetValue<string>("WordsApi:Uri")
                , Configuration.GetValue<string>("WordsApi:Host")
                , Configuration.GetValue<string>("WordsApi:Key")));
            services.AddSingleton<IWordRepository>(s => new MerriamWebsterDictionaryRepository(Configuration.GetValue<string>("MerriamWebsterDictionaryApi:Uri")
                , Configuration.GetValue<string>("MerriamWebsterDictionaryApi:Key")));
            services.AddSingleton<IUserWordService, UserWordService>();
            services.AddSingleton<IUserWordRepository>(s => new SqlUserWordRepository(Configuration.GetConnectionString("UserWordConnection")));
            //services.AddRazorPages();

           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
