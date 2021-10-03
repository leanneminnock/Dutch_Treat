using AutoMapper;
using Dutch_Treat.Data;
using Dutch_Treat.Data.Entities;
using Dutch_Treat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dutch_Treat
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config )
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DutchContext>();
            services.AddAuthentication().AddCookie()
                                        .AddJwtBearer(cfg => 
                                        {
                                            cfg.TokenValidationParameters = new TokenValidationParameters()
                                            {
                                                ValidIssuer = _config["Tokens:Issuer"],
                                                ValidAudience = _config["Tokens:Audience"],
                                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                                            };
                                        });
            services.AddTransient<DutchSeeder>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // add the interface as a service that can be used but use the class as the implementation
            services.AddScoped<IDutchRepository, DutchRepository>();
            services.AddDbContext<DutchContext>();
            services.AddTransient<IMailService, NullMailService>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(cfg =>
            {
                cfg.MapRazorPages();
                cfg.MapControllerRoute("Default", "/{controller}/{action}/{id?}", new { controller = "App", action = "Index" });  //incase nothing is defined the root has a default here.
            });


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("<html><body><h1>Hello Pluralsight</html></body></h1>");
            //});
        }
    }
}
