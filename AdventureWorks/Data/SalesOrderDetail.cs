namespace AdventureWorks.Data
{
    public class SalesOrderDetail
    {
        public int SalesOrderDetailID { get; set; }
        public int ProductID { get; set; } = 707;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
