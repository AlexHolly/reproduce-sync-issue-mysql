using AdventureWorks.Data;
using AdventureWorks.Service;
using Microsoft.AspNetCore.Components;

namespace AdventureWorks.Components
{
    public partial class SyncDataComponent : ComponentBase
    {
        [Inject] public SalesOrderDetailService SalesOrderDetailService { get; set; }

        public SalesOrderDetail SalesOrderDetail { get; set; }

        protected override void OnInitialized()
        {
            SalesOrderDetail = new SalesOrderDetail();
        }

        public void AddSalesOrderDetail()
        {
            SalesOrderDetailService.SaveSalesOrderDetail(SalesOrderDetail);
            SalesOrderDetail = new SalesOrderDetail();
        }
    }
}
