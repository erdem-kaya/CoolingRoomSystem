using Azure.Core;
using Business.Models.Booking;
using Business.Models.Payment;
using Data.Entities;

namespace Business.Factories;

public class BookingFactory
{
    public static BookingsEntity Create(BookingRegistrationForm form) => new()
    {
        BookingDate = form.BookingDate,
        StartDate = form.StartDate,
        FinishDate = form.FinishDate,
        Discount = form.Discount,
        TotalPrice = form.TotalPrice,
        Description = form.Description,
        BookedByUserId = form.BookedByUserId,
        PaymentId = form.PaymentId,
        CustomerId = form.CustomerId,
        CoolingRoomId = form.CoolingRoomId,
        CreatedAt = DateTime.UtcNow,
    };

    public static BookingForm Create(BookingsEntity entity) => new()
    {
        Id = entity.Id,
        BookingDate = entity.BookingDate,
        StartDate = entity.StartDate,
        FinishDate = entity.FinishDate,
        CreatedAt = entity.CreatedAt,
        Discount = entity.Discount,
        TotalPrice = entity.TotalPrice,
        Description = entity.Description,
        PaymentId = entity.PaymentId,
        CustomerId = entity.CustomerId,
        BookedByUserId = entity.BookedByUserId ?? 0,
        CoolingRoomId = entity.CoolingRoomId,
    };

    public static void Update(BookingsEntity entity, BookingUpdateForm form)
    {
        if (form.BookingDate != entity.BookingDate)
            entity.BookingDate = form.BookingDate;

        if (form.StartDate != entity.StartDate)
            entity.StartDate = form.StartDate;

        if (form.FinishDate != entity.FinishDate)
            entity.FinishDate = form.FinishDate;

        if (form.Discount != entity.Discount)
            entity.Discount = form.Discount;

        if (form.TotalPrice != entity.TotalPrice)
            entity.TotalPrice = form.TotalPrice;

        if (form.Description != entity.Description)
            entity.Description = form.Description;

        if (form.PaymentId != entity.PaymentId)
            entity.PaymentId = form.PaymentId;

        if (form.BookedByUserId != entity.BookedByUserId)
            entity.BookedByUserId = form.BookedByUserId;

        if(form.CustomerId != entity.CustomerId)
            entity.CustomerId = form.CustomerId;

        if (form.CoolingRoomId != entity.CoolingRoomId)
            entity.CoolingRoomId = form.CoolingRoomId;

        entity.UpdatedAt = DateTime.UtcNow;
    }

    public static BookingReportsForm CreateReports(BookingsEntity entity) => new()
    {
        Id = entity.Id,
        BookingDate = entity.BookingDate,
        StartDate = entity.StartDate,
        FinishDate = entity.FinishDate,
        CreatedAt = entity.CreatedAt,
        UpdateDate = entity.UpdatedAt ?? entity.CreatedAt,
        Discount = entity.Discount,
        TotalPrice = entity.TotalPrice,
        Description = entity.Description,
        PaymentId = entity.PaymentId,
        BookedByUserId = entity.BookedByUserId ?? 0,
        CustomerId = entity.CustomerId,
        CoolingRoomId = entity.CoolingRoomId,
    };

}
