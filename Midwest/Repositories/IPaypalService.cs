using System.Threading.Tasks;
using Midwest.Models.DTO;

namespace Midwest.Repositories
{
    public interface IPaypalService
    {
        Task<string> InitiatePaymentAsync(PaymentDataDto paymentData);
    }
}
