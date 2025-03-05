using Data.Entities;

namespace Data.Interfaces;

public interface IPaymentRepository : IBaseRepository<PaymentsEntity>
{
    // Get total revenue
    // Toplam geliri getir
    Task<decimal> GetTotalRevenueAsync();

    // Get payment by booking ID 
    // Rezervasyon ID'sine göre ödemeyi getir
    Task<PaymentsEntity> GetPaymentByBookingIdAsync(int bookingId);

    // Calculate the customer's remaining debt
    // Müşterinin kalan borcunu hesapla
    Task<decimal> CalculateRemainingDebtAsync(int customerId);
}
