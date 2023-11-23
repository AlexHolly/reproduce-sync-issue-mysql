using AdventureWorks.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AdventureWorks.Service
{
    public class SalesOrderDetailService
    {
        private AdventureWorksDbContext _adventureWorksDbContext;

        public SalesOrderDetailService(AdventureWorksDbContext adventureWorksDbContext)
        {
            _adventureWorksDbContext = adventureWorksDbContext;
        }

        public void SaveSalesOrderDetail(SalesOrderDetail salesOrderDetail)
        {
            _adventureWorksDbContext.SalesOrderDetail.Add(salesOrderDetail);

            int nextId = _adventureWorksDbContext.SalesOrderDetail.Select(a => a.SalesOrderDetailID).ToList().DefaultIfEmpty(0).Max();
            nextId += 1;

            string sql = $"INSERT INTO `SalesOrderDetail` VALUES({nextId}, {salesOrderDetail.ProductID}, \"{salesOrderDetail.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss")}\")";
            
            _adventureWorksDbContext.Database.ExecuteSqlRaw(sql);
        }
    }
}
