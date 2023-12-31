namespace Midwest.Models.DTO
{
    public class PaymentDataDto
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Contact { get; set; }
        public string Currency { get; set; }

        public int NoOfMachines { get; set; }

        
    }
}
