using Microsoft.AspNetCore.Components;
using SyncSetup;

namespace MariaDbServer.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public DatabaseSyncService DatabaseSyncService { get; set; }
        [Inject] public IConfiguration Configuration { get; set; }
        
        public void ExportAdventureWorksDbToSqlite()
        {
            var connectionString = Configuration.GetConnectionString("AdventureWorksDb");
            DatabaseSyncService.ExportAdventureWorksDbToSqliteAsync(connectionString);
        }
    }
}
