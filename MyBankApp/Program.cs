using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;

namespace MyBankApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<BankAppDataContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BankAppDataContext>();

            builder.Services.AddRazorPages();

            builder.Services.AddTransient<DataInitializer>();
            builder.Services.AddTransient<TransactionService>();
            builder.Services.AddTransient<CustomerService>();

            // Add Application Insights telemetry
            builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:InstrumentationKey"]);

            var app = builder.Build();

            // Configure logging
            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var initializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
                    initializer.SeedData();
                }
                catch (Exception ex)
                {
                    // Log error and re-throw to stop the application if the database initialization fails
                    Console.WriteLine($"An error occurred during database initialization: {ex.Message}");
                    throw; // Re-throw the exception to stop the application if the database initialization fails
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}