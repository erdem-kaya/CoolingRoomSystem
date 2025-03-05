using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class PaymentRepository(DataContext context) : BaseRepository<PaymentsEntity>(context), IPaymentRepository
{
    public async Task<decimal> CalculateRemainingDebtAsync(int customerId)
    {
        try
        {
            var totalPayment = await _context.Payments
                .Where(x => x.CustomerPaymentId == customerId)
                .SumAsync(x => x.Amount); // Veritabanında doğrudan SUM işlemi yap
            return totalPayment;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error calculating remaining debt : {ex.Message}");
            return 0;
        }
    }

    public async Task<PaymentsEntity> GetPaymentByBookingIdAsync(int bookingId)
    {
        try
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => _context.Bookings.Any(b => b.Id == bookingId && b.PaymentId == p.Id));

            if (payment == null)
            {
                Debug.WriteLine($"No payment found for booking ID: {bookingId}");
                return null!;
            }

            return payment;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting payments by booking ID {bookingId} : {ex.Message}");
            return null!;
        }
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        try
        {
            var totalRevenue = await _context.Payments
                .SumAsync(x => x.Amount);
            return totalRevenue;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting total revenue : {ex.Message}");
            return 0;
        }
    }
}
