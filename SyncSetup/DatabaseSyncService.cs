using Dotmim.Sync.Enumerations;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync;
using Dotmim.Sync.Web.Client;
using System.Data;
using Microsoft.Data.Sqlite;
using Dotmim.Sync.MySql;

namespace SyncSetup
{
    public class DatabaseSyncService
    {
        private int productId = 707;

        public void SyncDatabase()
        {
            Task.Run(async () =>
            {
                await SyncAdventureWorksDbAsync();
            });
        }

        public async Task SyncAdventureWorksDbAsync()
        {
            var sqliteDbConnectionString = "AdventureWorks.db";

            var clientProvider = new SqliteSyncProvider(sqliteDbConnectionString);

            Dotmim.Sync.SyncSetup setup = SyncDatabaseSetup();

            var options = new SyncOptions
            {
                BatchSize = 1000
            };

            var parameters = new SyncParameters
            {
                { "ProductId", productId }
            };

            var remoteOrchestrator = new WebRemoteOrchestrator("https://localhost:7167/api/syncDb");

            var agent = new SyncAgent(clientProvider, remoteOrchestrator, options);

            var progress = new SynchronousProgress<ProgressArgs>(args => Console.WriteLine($"{args.ProgressPercentage:p}:\t{args.Message}"));
            await agent.SynchronizeAsync(setup, parameters, progress);

            agent.Dispose();
            SqliteConnection.ClearAllPools();
        }

        public static Dotmim.Sync.SyncSetup SyncDatabaseSetup()
        {
            // Tables involved in the sync process
            var setup = new Dotmim.Sync.SyncSetup("salesorderdetail");

            setup.Tables["salesorderdetail"].SyncDirection = SyncDirection.Bidirectional;

            // Add filters Example
            var filter = new SetupFilter("salesorderdetail");
            filter.AddParameter("ProductID", DbType.String);
            filter.AddWhere("ProductID", "salesorderdetail", "ProductID");

            setup.Filters.Add(filter);

            return setup;
        }

        public async Task ExportAdventureWorksDbToSqliteAsync(string adventureWorksMariaDbConnectionString)
        {
            string sqliteDbConnectionString = $"Data Source=..\\SqliteClient\\AdventureWorks.db";

            var clientProvider = new SqliteSyncProvider(sqliteDbConnectionString);
            var serverProvider = new MySqlSyncProvider(adventureWorksMariaDbConnectionString);

            Dotmim.Sync.SyncSetup setup = ExportDatabaseSetup();

            var agent = new SyncAgent(clientProvider, serverProvider);

            await agent.SynchronizeAsync("ExportToSqlite", setup);

            agent.Dispose();
            SqliteConnection.ClearAllPools();
        }

        public static Dotmim.Sync.SyncSetup ExportDatabaseSetup()
        {
            // Tables involved in the sync process
            var setup = new Dotmim.Sync.SyncSetup("salesorderdetail");

            setup.Tables["salesorderdetail"].SyncDirection = SyncDirection.DownloadOnly;

            return setup;
        }
    }
}
