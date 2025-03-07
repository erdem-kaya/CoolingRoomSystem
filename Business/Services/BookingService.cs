using Business.Factories;
using Business.Interfaces;
using Business.Models.Booking;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class BookingService(BookingRepository bookingRepository) : IBookingService
{
    private readonly BookingRepository _bookingRepository = bookingRepository;

    public async Task<BookingForm> CreateAsync(BookingRegistrationForm form)
    {
        if (form == null)
            throw new ArgumentNullException(nameof(form), "Booking form cannot be null.");
        await _bookingRepository.BeginTransactionAsync();

        try
        {
            var booking = BookingFactory.Create(form);
            booking.UpdatedAt = DateTime.UtcNow;
            var createdBooking = await _bookingRepository.CreateAsync(booking);
            var result = createdBooking != null ? BookingFactory.Create(createdBooking) : null!;
            await _bookingRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating booking: {ex.Message}");
            await _bookingRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<IEnumerable<BookingForm>> GetAllAsync()
    {
        try
        {
            var allBookings = await _bookingRepository.GetAllAsync();
            var result = allBookings.Select(BookingFactory.Create).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all bookings: {ex.Message}");
            return [];
        }
    }

    public async Task<BookingForm> GetByIdAsync(int id)
    {
        try
        {
            var getBooking = await _bookingRepository.GetItemAsync(x => x.Id == id);
            var result = getBooking != null ? BookingFactory.Create(getBooking) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting booking by id: {ex.Message}");
            return null!;
        }
    }

    public async Task<BookingForm> UpdateAsync(BookingUpdateForm form)
    {
        await _bookingRepository.BeginTransactionAsync();
        try
        {
            var findBooking = await _bookingRepository.GetItemAsync(x => x.Id == form.Id) ?? throw new Exception($"Booking with ID {form.Id} not found.");
            BookingFactory.Update(findBooking, form);
            findBooking.UpdatedAt = DateTime.UtcNow;
            var updatedBooking = await _bookingRepository.UpdateAsync(x => x.Id == form.Id, findBooking);
            var result = updatedBooking != null ? BookingFactory.Create(updatedBooking) : null!;
            await _bookingRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating booking: {ex.Message}");
            await _bookingRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await _bookingRepository.BeginTransactionAsync();

        try
        {
            var deletedBooking = await _bookingRepository.DeleteAsync(x => x.Id == id);
            if (!deletedBooking)
                throw new Exception($"Error deleting booking with ID {id}.");

            await _bookingRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting booking: {ex.Message}");
            await _bookingRepository.RollbackTransactionAsync();
            return false;
        }
    }

    public async Task<IEnumerable<BookingReportsForm>> GetBookingsByStartDateAsync(DateTime startDate)
    {
        try
        {
            var getBookingsByStartDate = await _bookingRepository.GetBookingsByStartDateAsync(startDate);
            var result = getBookingsByStartDate.Select(BookingFactory.CreateReports).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by check-in date : {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingReportsForm>> GetBookingsByFinishDateAsync(DateTime finishDate)
    {
        try
        {
            var getBookingsByFinishDate = await _bookingRepository.GetBookingsByFinishDateAsync(finishDate);
            var result = getBookingsByFinishDate.Select(BookingFactory.CreateReports).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by check-out date : {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingReportsForm>> GetBookingsBetweenStartDateFinishDateAsync(DateTime startDate, DateTime finishDate)
    {
        try
        {
            var getBookingsBetweenStartDateFinishDate = await _bookingRepository.GetBookingsBetweenStartDateFinishDateAsync(startDate, finishDate);
            var result = getBookingsBetweenStartDateFinishDate.Select(BookingFactory.CreateReports).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings between check-in and check-out date : {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingReportsForm>> GetBookingsByPaymentStatusAsync(int paymentStatusId)
    {
        try
        {
            var getBookingsByPaymentStatus = await _bookingRepository.GetBookingsByPaymentStatusAsync(paymentStatusId);
            var result = getBookingsByPaymentStatus.Select(BookingFactory.CreateReports).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by payment status : {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingReportsForm>> GetCustomerBookingsAsync(int customerId)
    {
        try
        {
            var getCustomerBookings = await _bookingRepository.GetCustomerBookingsAsync(customerId);
            var result = getCustomerBookings.Select(BookingFactory.CreateReports).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting customer bookings : {ex.Message}");
            return [];
        }
    }
}
