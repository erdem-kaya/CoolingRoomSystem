using Business.Models.Booking;

namespace Business.Interfaces;

public interface IBookingService
{
    Task<BookingForm> CreateAsync(BookingRegistrationForm form);
    Task<IEnumerable<BookingForm>> GetAllAsync();
    Task<BookingForm> GetByIdAsync(int id);
    Task<BookingForm> UpdateAsync(BookingUpdateForm form);
    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<BookingReportsForm>> GetBookingsByStartDateAsync(DateTime startDate);

    //Get bookings by check-out date
    //CheckOut tarihine göre rezervasyonları getir

    Task<IEnumerable<BookingReportsForm>> GetBookingsByFinishDateAsync(DateTime finishDate);

    //Get bookings between check-in and check-out date
    //CheckIn ve CheckOut tarihleri arasındaki rezervasyonları getir
    Task<IEnumerable<BookingReportsForm>> GetBookingsBetweenStartDateFinishDateAsync(DateTime startDate, DateTime finishDate);

    //Get bookings by payment status
    //Ödeme durumuna göre rezervasyonları getir
    Task<IEnumerable<BookingReportsForm>> GetBookingsByPaymentStatusAsync(int paymentStatusId);

    // Get by customer all bookings
    // Müşterinin tüm rezervasyonlarını getir
    Task<IEnumerable<BookingReportsForm>> GetCustomerBookingsAsync(int customerId);
}
