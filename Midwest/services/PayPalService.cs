using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PayPal;
using PayPal.Api;
using Midwest.Models.DTO;
using Midwest.Repositories;

public class PayPalService : IPaypalService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<PayPalService> _logger;
    public PayPalService(IConfiguration configuration, ILogger<PayPalService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public async Task<string> InitiatePaymentAsync(PaymentDataDto paymentData)
    {
        try
        {
            var config = ConfigManager.Instance.GetProperties();
            config.Add("mode", "sandbox"); // Use "live" for production
            var accessToken = await Task.Run(() => new OAuthTokenCredential(
                _configuration["PayPal:ClientId"],
                _configuration["PayPal:ClientSecret"]
            ).GetAccessToken());
            var apiContext = new APIContext(accessToken)
            {
                Config = config
            };
            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            currency = paymentData.Currency,
                            total = paymentData.TotalAmount.ToString("F2"),
                        },
                    },
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = "http://localhost:4200/payment-success",
                    cancel_url = "http://localhost:4200/payment-canceled",
                },
            };
            var createdPayment = await Task.Run(() => payment.Create(apiContext));
            var approvalUrl = createdPayment.links
                .Find(link => link.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase));
            if (approvalUrl != null)
            {
                return approvalUrl.href;
            }
            else
            {
                _logger.LogError("PayPal Payment Creation Failed: No approval URL received.");
                return null;
            }
        }
        catch (PayPalException ex)
        {
            _logger.LogError(ex, "PayPal Payment Creation Failed");
            return null;
        }
    }
}
