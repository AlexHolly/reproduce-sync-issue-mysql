using SyncSetup;
using Microsoft.EntityFrameworkCore;
using AdventureWorks.Data;
using AdventureWorks.Service;
using Dotmim.Sync;
using Dotmim.Sync.MySql;

namespace MariaDbServer
{
    public static class Program
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
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            builder.Services.AddDatabaseSync(builder.Configuration);

            builder.Services.AddControllers();

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

            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }

        public static void AddDatabaseSync(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));

            var adventureWorksDbConnectionString = configuration.GetConnectionString("AdventureWorksDb");

            var options = new SyncOptions
            {
                SnapshotsDirectory = "C:\\Tmp\\Snapshots",
                BatchSize = 2000
            };

            var setupUserDb = DatabaseSyncService.SyncDatabaseSetup();

            services.AddSyncServer<MySqlSyncProvider>(adventureWorksDbConnectionString, setupUserDb, options);
        }
    }
}