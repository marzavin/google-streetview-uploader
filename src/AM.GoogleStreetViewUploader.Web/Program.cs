//using Google.Apis.Auth.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace AM.GoogleStreetViewUploader.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            services.Configure<CookiePolicyOptions>(options => { options.Secure = CookieSecurePolicy.Always; });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;// GoogleOpenIdConnectDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = GoogleDefaults.AuthenticationScheme;//GoogleOpenIdConnectDefaults.AuthenticationScheme;
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                });

            services.AddControllersWithViews();

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(name: "default", pattern: "{controller=Upload}/{action=Index}");

            app.Run();
        }
    }
}
