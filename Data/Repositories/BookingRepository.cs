using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class BookingRepository(DataContext context) : BaseRepository<BookingsEntity>(context), IBookingRepository
{
    public async Task<IEnumerable<BookingsEntity>> GetBookingsBetweenStartDateFinishDateAsync(DateTime startDate, DateTime finishDate)
    {
        try
        {
            var start = startDate.Date;
            var end = finishDate.Date.AddDays(1).AddTicks(-1); // Burada bitiş tarihine bir gün ekleyip bir tick çıkarıyoruz. Böylece bitiş tarihini de dahil ediyoruz.
            
            var bookings = await _context.Bookings
                          .Where(x => x.StartDate >= start && x.FinishDate <= end)
                          .ToListAsync();
            return bookings;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings between check-in and check-out date : {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingsEntity>> GetBookingsByStartDateAsync(DateTime startDate)
    {
        try
        {
            var start = startDate.Date;
            var end = startDate.Date.AddDays(1).AddTicks(-1);

            var bookings = await _context.Bookings
                          .Where(x => x.StartDate >= start && x.StartDate <= end)
                          .ToListAsync();
            return bookings;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by check-in date : {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingsEntity>> GetBookingsByFinishDateAsync(DateTime finishDate)
    {
        try
        {
            var finish = finishDate.Date;
            var end = finishDate.Date.AddDays(1).AddTicks(-1);

            var bookings = await _context.Bookings
                          .Where(x => x.FinishDate >= finish && x.FinishDate <= end )
                          .ToListAsync();
            return bookings;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by check-out date : {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<BookingsEntity>> GetBookingsByPaymentStatusAsync(int paymentStatusId)
    {
        try
        {
            var bookings = await _context.Bookings
                          .Where(p => p.Payment != null && p.Payment.PaymentStatusId == paymentStatusId)
                          .Include(p => p.Payment)
                          .ToListAsync();
            return bookings;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by payment status : {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingsEntity>> GetCustomerBookingsAsync(int customerId)
    {
        try
        {
            var bookings = await _context.Bookings
                          .Where(x => x.CustomerId == customerId)
                          .Include(x => x.Payment)
                          .Include(x => x.CoolingRoom)
                          .ToListAsync();
            return bookings;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting customer bookings : {ex.Message}");
            return [];
        }
    }
}
