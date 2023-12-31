using Microsoft.AspNetCore.Mvc;
using Midwest.Data;
using Midwest.Models.Domain;
using Midwest.Models.DTO;
using Midwest.Repositories;

namespace Midwest.services
{
    public class LicenseService : ILicenseService
    {
        private readonly MidWestDBContext _context;
        public LicenseService(MidWestDBContext context)
        {
            _context = context;
        }
        public IActionResult SaveLicenseDetails(LicenseDetailsDto licenseDetails)
        {
            try
            {
                licenseDetails.LicenseKey = GenerateLicenseKey(); // No need to convert to string
                licenseDetails.StartDate = DateTime.Now;
                licenseDetails.ExpiryDate = CalculateExpiryDate(licenseDetails.StartDate);
                licenseDetails.PurchaseDate = DateTime.Now; // You can modify this as needed
                licenseDetails.Active = true;
                var client = new tblClients
                {
                    ClientName = licenseDetails.ClientName,
                    Amount = licenseDetails.Amount, 
                    NoOfMachines = licenseDetails.NoOfMachines,
                    Address = licenseDetails.Address,
                    City = licenseDetails.City,
                    Country = licenseDetails.Country,
                    ZipCode = licenseDetails.ZipCode,
                    EmailID = licenseDetails.EmailID,
                    ContactNo = licenseDetails.ContactNo,
                    LicenseKey = licenseDetails.LicenseKey,
                    StartDate = licenseDetails.StartDate,
                    ExpiryDate = licenseDetails.ExpiryDate,
                    PurchaseDate = licenseDetails.PurchaseDate,
                    Active = licenseDetails.Active
                };

                var license = new tblLicenses
                {
                    LicenseKey = licenseDetails.LicenseKey,
                    Active = true,
                    ExpiryDate = licenseDetails.ExpiryDate,
                    MaxUnits = licenseDetails.NoOfMachines, // Set the maximum allowed units
                    CurrentUnits = 0, // Initially, no units are in use
                    Client = client // Link the license to the client
                };
                _context.tblClients.Add(client);
                _context.tblLicenses.Add(license);

                _context.SaveChanges();
                return new OkObjectResult("License details saved successfully");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500); // Return 500 Internal Server Error on failure
            }
        }

        private Guid GenerateLicenseKey()
        {
            return Guid.NewGuid();
        }
        
        private DateTime CalculateExpiryDate(DateTime startDate)
        {
            return startDate.AddYears(1);
        }
    }
}
