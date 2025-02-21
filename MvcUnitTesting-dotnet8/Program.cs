using Microsoft.EntityFrameworkCore;
using MvcUnitTesting_dotnet8.Data;
using MvcUnitTesting_dotnet8.Models;

namespace MvcUnitTesting_dotnet8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Configure the DbContext with EnableRetryOnFailure
            builder.Services.AddDbContext<BookDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure()));

            // Register repository and seeder as services
            builder.Services.AddScoped<IRepository<Book>, WorkingBookRepository<Book>>();
            builder.Services.AddScoped<BookDbSeeder>();

            var app = builder.Build();

            // Seed the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeder = services.GetRequiredService<BookDbSeeder>();
                seeder.Seed();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
