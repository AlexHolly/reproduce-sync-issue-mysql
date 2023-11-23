using AdventureWorks.Data;
using AdventureWorks.Service;
using Microsoft.EntityFrameworkCore;
using SyncSetup;

namespace SqliteClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<SalesOrderDetailService>();
            builder.Services.AddSingleton<DatabaseSyncService>();

            var connectionString = builder.Configuration.GetConnectionString("AdventureWorksDb");
            builder.Services.AddDbContext<AdventureWorksDbContext>(opt =>
            {
                opt.UseSqlite(connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}