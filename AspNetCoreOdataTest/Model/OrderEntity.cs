namespace AspNetCoreOdataTest.Model
{
    public class OrderEntity
    {
        public int OrderId { get; set; }

        public string OrderName { get; set; }

        public int OrderQuantity { get; set; }

        public string DebugMessage { get; set; }

        public string GraphProperty { get; set; }

        public AddressEntity Address { get; set; }

        public OrderEntity(int id, string name, int quantity, string debugMessage, string city)
        {
            OrderId = id;
            OrderName = name;
            OrderQuantity = quantity;
            DebugMessage = debugMessage;
            Address = new AddressEntity
            {
                City = city
            };
        }
    }
}
