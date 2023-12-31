using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Midwest.Data;
using Midwest.Models.Domain;
using System;
using System.Linq;

namespace Midwest.services
{
    public class LicenseValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }

    public class LicenseValidationService
    {
        private readonly MidWestDBContext _context;
        private readonly ILogger<LicenseValidationService> _logger;

        public LicenseValidationService(MidWestDBContext context, ILogger<LicenseValidationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public LicenseValidationResult ActivateLicense(Guid licenseKey, string deviceName, string deviceID)
        {
            var client = _context.tblClients.FirstOrDefault(c => c.LicenseKey == licenseKey);
            if (client == null)
            {
                return new LicenseValidationResult { IsValid = false, Message = "License Key is not valid!" };
            }
            if (!client.Active)
            {
                return new LicenseValidationResult { IsValid = false, Message = "License is not active!" };
            }
            if (client.ExpiryDate < DateTime.Now)
            {
                return new LicenseValidationResult { IsValid = false, Message = "License is expired!" };
            }
            var license = _context.tblLicenses
                .Include(l => l.ActivatedDevices) // Include activated devices in the query
                .FirstOrDefault(c => c.LicenseKey == licenseKey);
            if (license == null)
            {
                return new LicenseValidationResult { IsValid = false, Message = "License not found for the given LicenseKey!" };
            }
            if (license.CurrentUnits >= license.MaxUnits)
            {
                return new LicenseValidationResult { IsValid = false, Message = "Activation limit of license key exhausted!" };
            }
            var device_ID = _context.ActivatedDevice.FirstOrDefault(c => c.DeviceID == deviceID);
            if (device_ID == null)
            {
                var activatedDevice = new ActivatedDevice
                {
                    DeviceName = deviceName,
                    DeviceID = deviceID,
                    ClientID = client.ClientID,
                    DeviceActive = client.Active,
                };
                license.ActivatedDevices.Add(activatedDevice);
                license.CurrentUnits++;
                license.Active = true; // Set the license as active
                _context.SaveChanges();
                return new LicenseValidationResult { IsValid = true, Message = "License activation successful!" };
            }
            else
            {
                return new LicenseValidationResult { IsValid = false, Message = "Device already activated!" };
            }
        }
        public LicenseValidationResult DeactivateLicense(Guid licenseKey, string deviceID)
        {
            var license = _context.tblLicenses.FirstOrDefault(l => l.LicenseKey == licenseKey);

            var device = _context.ActivatedDevice.FirstOrDefault(c => c.DeviceID == deviceID && c.License.LicenseKey == licenseKey);


            if (license == null)
            {
                return new LicenseValidationResult { IsValid = false, Message = "License Key is not valid!" };
            }
            if (device != null)
            {
                var activatedDevice = new ActivatedDevice
                {
                    DeviceActive = false
                };
                if (license.CurrentUnits > 0)
                {
                    license.CurrentUnits--;
                    _context.SaveChanges();
                    return new LicenseValidationResult { IsValid = true, Message = "License deactivation successful!" };
                }
                else
                {
                    return new LicenseValidationResult { IsValid = false, Message = "Device already activated!" };
                }
            }
            else
            {
                return new LicenseValidationResult { IsValid = false, Message = "Device already activated!" };
            }
        }
    }
}





