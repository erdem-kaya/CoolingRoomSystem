using Business.Models.Customer;
using Business.Models.Payment;

namespace Business.Interfaces;

public interface IPaymentService
{
    Task<PaymentForm> CreateAsync(PaymentRegistrationForm form);
    Task<IEnumerable<PaymentForm>> GetAllAsync();
    Task<PaymentForm> GetByIdAsync(int id);
    Task<PaymentForm> UpdateAsync(PaymentUpdateForm form);
    Task<bool> DeleteAsync(int id);
    Task<decimal> CalculateRemainingDebtAsync(int customerId);
    Task<PaymentReportForm> GetPaymentByBookingIdAsync(int bookingId);
    Task<decimal> GetTotalRevenueAsync();
}
