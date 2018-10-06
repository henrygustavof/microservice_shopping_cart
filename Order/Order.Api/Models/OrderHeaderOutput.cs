namespace Order.Api.Models
{
    public class OrderHeaderOutput
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Currency { get; set; }
    }
}
