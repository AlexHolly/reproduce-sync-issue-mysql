using Microsoft.AspNetCore.Components;
using SyncSetup;

namespace SqliteClient.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public DatabaseSyncService DatabaseSyncService { get; set; }

        public void Sync()
        {
            DatabaseSyncService.SyncDatabase();
        }
    }
}
