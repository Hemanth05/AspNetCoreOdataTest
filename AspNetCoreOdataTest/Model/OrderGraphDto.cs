namespace AspNetCoreOdataTest.Model
{
    public class OrderGraphDto : OrderDto
    {
        public string GraphProperty { get; set; }

        public AddressDto Address { get; set; }
    }
}
