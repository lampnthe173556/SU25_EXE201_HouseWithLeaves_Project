using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Helper.Email;
using ProjectHouseWithLeaves.Helper.Mapping;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.EmailService;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Text.Json.Serialization;

namespace ProjectHouseWithLeaves
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Register SQL
            builder.Services.AddDbContext<MiniPlantStoreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBDefault")));
            #endregion

            #region IConfiguration
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            #endregion

            #region Register DI
            // Client Services
            builder.Services.AddScoped<Services.ModelService.IProductService, Services.ModelService.ProductService>();
            builder.Services.AddScoped<Services.ModelService.ICategoryService, Services.ModelService.CategoryService>();
            builder.Services.AddScoped<IContactService, ContactService>();
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<IAuthenticationServices, AuthenticationService>();
            builder.Services.AddScoped<IUserService, UserService>();

           
            #endregion

            #region Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(72);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            #endregion

            #region mapper
            builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
            builder.Services.AddAutoMapper(typeof(UserMappingProfile));
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            #endregion

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Lax;
            })
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Google:ClientId"];
                options.ClientSecret = builder.Configuration["Google:ClientSecret"];
                options.CallbackPath = "/Authen/GoogleCallback";
            });
            builder.Services.AddHttpContextAccessor();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
        
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");         
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Home}/{id?}");

            app.Run();
        }
    }
}
