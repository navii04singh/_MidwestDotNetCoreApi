using Midwest.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class tblLicenses
{
    [Key]
    public Guid LicenseKey { get; set; } // Primary Key

    public int ClientID { get; set; } // Foreign Key to tblClients

    // Updated to support multiple activated devices
    public ICollection<ActivatedDevice> ActivatedDevices { get; set; } = new List<ActivatedDevice>();

    [Required]
    public bool Active { get; set; }
    [Required]
    public DateTime ExpiryDate { get; set; }
    [Required]
    public int MaxUnits { get; set; }
    [Required]
    public int CurrentUnits { get; set; }

    [ForeignKey("ClientID")]
    public tblClients Client { get; set; }
}
