using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;

using MyDictionary.UI.Data;
using MyDictionary.UI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyDictionary.Infrastructure.Repositories;
using MyDictionary.Infrastructure.Interfaces;
using MyDictionary.Infrastructure.Services;
using MyDictionary.Infrastructure.Respositories;

namespace MyDictionary.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "401161494104-mre6m0s658al274m4pkr1dstnuc4s0c1.apps.googleusercontent.com";
                    options.ClientSecret = "KxHTinKILojvjGLKCd8fL07B";
                });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddSingleton<IWordLookupRepository>(s => new RapidApiRepository(
                Configuration.GetValue<string>("WordsApi:Uri")
                , Configuration.GetValue<string>("WordsApi:Host")
                , Configuration.GetValue<string>("WordsApi:Key")));
            services.AddSingleton<IWordRepository>(s => new MerriamWebsterDictionaryRepository(Configuration.GetValue<string>("MerriamWebsterDictionaryApi:Uri")
                , Configuration.GetValue<string>("MerriamWebsterDictionaryApi:Key")));
            services.AddSingleton<IUserWordService, UserWordService>();
            services.AddSingleton<IUserWordRepository>(s => new SqlUserWordRepository(Configuration.GetConnectionString("UserWordConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}