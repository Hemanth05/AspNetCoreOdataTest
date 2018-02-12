namespace AspNetCoreOdataTest.Model
{
    public class OrderGraphDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string DebugMessage { get; set; }

        public string CityAddress { get; set; }

        public string GraphProperty { get; set; }

        public AddressDto Address { get; set; }
    }
}
