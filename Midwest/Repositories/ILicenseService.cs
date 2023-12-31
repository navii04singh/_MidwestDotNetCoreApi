using Microsoft.AspNetCore.Mvc;
using Midwest.Models.DTO;

namespace Midwest.Repositories
{
    public interface ILicenseService
    {
        IActionResult SaveLicenseDetails(LicenseDetailsDto licenseDetails);
    }
}
