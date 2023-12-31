using Microsoft.AspNetCore.Mvc;
using Midwest.Models.DTO;
using Midwest.services;
using System;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/licenses")]
public class NewLicenseController : ControllerBase
{
    private readonly LicenseService _licenseService;
    private readonly LicenseValidationService _licenseValidationService;

    public NewLicenseController(LicenseService licenseService, LicenseValidationService licenseValidationService)
    {
        _licenseService = licenseService;
        _licenseValidationService = licenseValidationService;
    }

    [HttpPost("save-license-details")]
    public IActionResult SaveLicenseDetails([FromBody] LicenseDetailsDto licenseDetails)
    {
        try
        {
            _licenseService.SaveLicenseDetails(licenseDetails);
            return Ok(new { message = "License details saved successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "License details save failed" });
        }
    }

    [HttpGet("validate-license")]
    public IActionResult ValidateLicense([FromQuery] Guid licenseKey, [FromQuery] string deviceName, [FromQuery] string deviceID)
    {
        var validationResult = _licenseValidationService.ActivateLicense(licenseKey, deviceName, deviceID);

        if (validationResult.IsValid)
        {
            return Ok(new { isValid = true });
        }
        else
        {
            return BadRequest(validationResult.Message);
        }
    }



    [HttpPut("deactivate-license")]
    public IActionResult DeactivateLicense([FromQuery] Guid licenseKey, [FromQuery] string deviceID)
    {
        var isDeactivated = _licenseValidationService.DeactivateLicense(licenseKey, deviceID);
        if (isDeactivated.IsValid)
        {
            return Ok(isDeactivated.Message);
        }
        else
        {
            return BadRequest(isDeactivated.Message);
        }
    }
}
