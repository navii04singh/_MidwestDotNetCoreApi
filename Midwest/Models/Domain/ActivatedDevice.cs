using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Midwest.Models.Domain
{
    public class ActivatedDevice
    {
        public int ActivatedDeviceID { get; set; }
        public string DeviceName { get; set; }
        public string DeviceID { get; set; }
        // Add other properties as needed

        // Foreign key to link to the parent license
        public int ClientID { get; set; }

        public bool DeviceActive { get; set; }
        public tblLicenses License { get; set; }
    }

}
