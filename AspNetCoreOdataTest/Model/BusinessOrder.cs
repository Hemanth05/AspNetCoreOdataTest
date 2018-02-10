namespace AspNetCoreOdataTest.Model
{
    public class BusinessOrder
    {
        public int OrderId { get; set; }

        public string OrderName { get; set; }

        public int OrderQuantity { get; set; }

        public string DebugMessage { get; set; }

        public BusinessOrder(int id, string name, int quantity, string debugMessage)
        {
            OrderId = id;
            OrderName = name;
            OrderQuantity = quantity;
            DebugMessage = debugMessage;
        }
    }
}
