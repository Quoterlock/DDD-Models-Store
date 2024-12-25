using DddModelsStore.DataAccess.Entities;
using DddModelsStore.DataAccess;
using DddModelsStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DddModelsStore
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            await Seed(app.Services);
            ConfigureRequestPipeline(app);
            app.Run();
        }

        private static async Task Seed(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                
                // seed default roles
                var roles = new[]
                {
                    Roles.Administrator.ToString(),
                    Roles.Moderator.ToString(),
                    Roles.User.ToString(),
                };

                foreach (var role in roles)
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                
                // seed default admin
                var email = "admin@mail.com";
                var password = "!Admin1234";
                
                // check if already exists
                if (await userManager.FindByEmailAsync(email) != null)
                    return;
                
                var user = new AppUser()
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                };
                
                // seed
                var result = await userManager.CreateAsync(user, password);
                if(result.Succeeded)
                    await userManager.AddToRoleAsync(
                        user, Roles.Administrator.ToString());
                else
                    throw new Exception(result.Errors.First().Description);
            }   
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            var usersDbConnectionString = builder.Configuration.GetConnectionString("IdentityDbContextConnection") 
                ?? throw new InvalidOperationException(
                     "Connection string 'IdentityDbContextConnection' not found.");
            builder.Services.AddDbContext<UserIdentityDbContext>(options => options.UseSqlServer(usersDbConnectionString));
            builder.Services.AddDefaultIdentity<AppUser>(options =>
                    options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserIdentityDbContext>();

            var mainDbConnectionString = builder.Configuration.GetConnectionString("MainDbContextConnection"); 
            builder.Services.AddDbContext<MainDbContext>(options => 
                options.UseSqlServer(mainDbConnectionString)); 
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Config auth
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // password requirements
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;

                // lock setting (prevent bruteforce)
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // username settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz1234567890_.-@";
            });

            // Config cookies
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.LogoutPath = "/Identity/Account/Logout";
            });
        }

        private static void ConfigureRequestPipeline(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "area_default",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        }
    }
}






