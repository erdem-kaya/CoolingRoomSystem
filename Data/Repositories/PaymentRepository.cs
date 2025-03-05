using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class PaymentRepository(DataContext context) : BaseRepository<PaymentsEntity>(context), IPaymentRepository
{
    public Task<decimal> CalculateRemainingDebtAsync(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentsEntity> GetPaymentByBookingIdAsync(int bookingId)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetTotalRevenueAsync()
    {
        throw new NotImplementedException();
    }
}
