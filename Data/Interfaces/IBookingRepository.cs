using Data.Entities;

namespace Data.Interfaces;

public interface IBookingRepository : IBaseRepository<BookingsEntity>
{

    //Get bookings by check-in date
    //CheckIn tarihine göre rezervasyonları getir
    Task<IEnumerable<BookingsEntity>> GetBookingsByStartDateAsync(DateTime startDate);

    //Get bookings by check-out date
    //CheckOut tarihine göre rezervasyonları getir

    Task<IEnumerable<BookingsEntity>> GetBookingsByFinishDateAsync(DateTime finishDate);

    //Get bookings between check-in and check-out date
    //CheckIn ve CheckOut tarihleri arasındaki rezervasyonları getir
    Task<IEnumerable<BookingsEntity>> GetBookingsBetweenStartDateFinishDateAsync(DateTime startDate, DateTime finishDate);

    //Get bookings by payment status
    //Ödeme durumuna göre rezervasyonları getir
    Task<IEnumerable<BookingsEntity>> GetBookingsByPaymentStatusAsync(int paymentStatusId);

    // Get by customer all bookings
    // Müşterinin tüm rezervasyonlarını getir
    Task<IEnumerable<BookingsEntity>> GetCustomerBookingsAsync(int customerId);
}
