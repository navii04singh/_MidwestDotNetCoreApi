using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Midwest.Models.DTO;
using Newtonsoft.Json.Linq;
using Midwest.Repositories;
using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Midwest.Controllers
{
    [ApiController]
    [Route("api/paypal")]
    public class PaypalController : ControllerBase
    {
        private readonly IPaypalService _paypalService;
        private readonly ILogger<PaypalController> _logger;
        private readonly ILicenseService _licenseService;

        public PaypalController(IPaypalService paypalService, ILogger<PaypalController> logger, ILicenseService licenseService)
        {
            _paypalService = paypalService;
            _logger = logger;
            _licenseService = licenseService;
        }
        [HttpPost("initiate-payment")]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentDataDto paymentData)
        {
            var response = new ResponseModel();
            try
            {
                string paymentUrl = await _paypalService.InitiatePaymentAsync(paymentData);
                if (paymentUrl != null)
                {
                    response.Success = true;
                    response.Message = "url generated";
                    response.Data = paymentUrl;
                    
                }
                else
                {
                    response.Success = false;
                    response.Message = "Payment initiation failed";
                    response.Data = "500";                    
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Payment initiation failed: {ex.Message}");
            }
            return Ok(response);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> HandleWebhook()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string jsonBody = await reader.ReadToEndAsync();
                    _logger.LogInformation($"Received PayPal webhook notification: {jsonBody}");

                    // Check if the request body is empty
                    if (string.IsNullOrWhiteSpace(jsonBody))
                    {
                        _logger.LogInformation("Received an empty PayPal webhook notification.");
                        return Ok("Empty webhook request received.");
                    }

                    // Parse the JSON body to access webhook data
                    var webhookData = JObject.Parse(jsonBody);

                    // You can now access specific data from the webhook payload
                    string eventType = webhookData["event_type"]?.ToString();
                    string eventSummary = webhookData["summary"]?.ToString();

                    _logger.LogInformation($"Webhook event type: {eventType}, event summary: {eventSummary}");

                    // Handle the webhook event based on eventType and eventSummary
                    if (eventType == "PAYMENT.CAPTURE.COMPLETED")
                    {
                        _logger.LogInformation("Payment capture completed. Event summary: " + eventSummary);

                        var paymentDetails = webhookData["resource"]["amount"];
                        var clientName = webhookData["resource"]["payer"]["name"]["given_name"]?.ToString();
                        var totalAmountString = paymentDetails?["value"]?.ToString();

                        if (!string.IsNullOrWhiteSpace(clientName) && !string.IsNullOrWhiteSpace(totalAmountString) && decimal.TryParse(totalAmountString, out decimal totalAmount))
                        {
                            var licenseDetails = new LicenseDetailsDto
                            {
                                ClientName = clientName,
                                Amount = totalAmount,
                                // Add other properties based on webhookData as needed
                            };

                            var licenseResult = _licenseService.SaveLicenseDetails(licenseDetails);
                            if (licenseResult is OkResult)
                            {
                                _logger.LogInformation("License details saved successfully.");
                                return Ok();
                            }
                            else
                            {
                                _logger.LogError("License details save failed.");
                                return StatusCode(500, "License details save failed.");
                            }
                        }
                        else
                        {
                            _logger.LogError("Invalid or missing data in webhook payload.");
                            return StatusCode(400, "Invalid or missing data in webhook payload.");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Webhook event type not handled: " + eventType);
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing PayPal webhook: {ex.Message}");
                return StatusCode(500, $"Error processing PayPal webhook: {ex.Message}");
            }
        }


    }
}
