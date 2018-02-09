namespace AspNetCoreOdataTest.Model
{
    public class BusinessOrder
    {
        public int OrderId { get; set; }

        public string OrderName { get; set; }

        public int OrderQuantity { get; set; }

        public string DebugHelper => StaticDebugHelper;

        public static string StaticDebugHelper { get; set; }
    }
}
