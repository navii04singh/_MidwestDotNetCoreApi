using System.ComponentModel.DataAnnotations;

namespace Midwest.Models.Domain
{
    public class tblClients
    {
        [Key]
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string? EmailID { get; set; }
        public string? ContactNo { get; set; }
        public Guid LicenseKey { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Active { get; set; }
        public int NoOfMachines { get; set; }

        public ICollection<tblLicenses> Licenses { get; set; }

    }
}
