using System.Globalization;
using KonkurCRM.Core.Convertor;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.Core.Services.Services;
using KonkurCRM.DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace HiradAcademiCRM.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CultureInfo.DefaultThreadCurrentCulture
                = CultureInfo.DefaultThreadCurrentUICulture
                    = PersianDateExtensionMethods.GetPersianCulture();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10080);
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddRazorPages();

            services.AddDataProtection()
                // This helps surviving a restart: a same app will find back its keys. Just ensure to create the folder.
                .PersistKeysToFileSystem(new DirectoryInfo("wwwroot/KeyDirectory/"))
                // This helps surviving a site update: each app has its own store, building the site creates a new app
                .SetApplicationName("ProjectName")
                .SetDefaultKeyLifetime(TimeSpan.FromMinutes(43200));

            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

            });

            #endregion

            #region DataBaseContext

            services.AddDbContext<KonkurCRMContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("HiradCRMConnection"));
            });

            #endregion

            #region IOCs

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IAdviserService, AdviserService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IPlanService, PlanService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IPayService, PayService>();
            services.AddTransient<ICallService, CallService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(

                    name: "MyAreas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(

                    name: "Default",
                    pattern: "{controller=AdviserPanel}/{action=Index}/{id?}"
                );
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
