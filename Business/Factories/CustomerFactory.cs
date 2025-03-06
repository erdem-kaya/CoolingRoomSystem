using Business.Models.Customer;
using Data.Entities;

namespace Business.Factories;

public class CustomerFactory
{
    public static CustomersEntity Create(CustomerRegistrationForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Address = form.Address,
        City = form.City,
        Email = form.Email,
        PhoneNumber = form.PhoneNumber
    };

    public static CustomerForm Create(CustomersEntity entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Address = entity.Address,
        City = entity.City,
        Email = entity.Email,
        PhoneNumber = entity.PhoneNumber
    };

    public static void Update(CustomersEntity entity, CustomerUpdateForm form)
    {
        if (!string.IsNullOrWhiteSpace(form.FirstName))
            entity.FirstName = form.FirstName;

        if (!string.IsNullOrWhiteSpace(form.LastName))
            entity.LastName = form.LastName;

        if (!string.IsNullOrWhiteSpace(form.Address))
            entity.Address = form.Address;

        if (!string.IsNullOrWhiteSpace(form.City))
            entity.City = form.City;

        if (!string.IsNullOrWhiteSpace(form.Email))
            entity.Email = form.Email;

        if (!string.IsNullOrWhiteSpace(form.PhoneNumber))
            entity.PhoneNumber = form.PhoneNumber;
    }

    public static CustomerReportsForm CreateReports(CustomersEntity entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Address = entity.Address,
        City = entity.City,
        Email = entity.Email,
        PhoneNumber = entity.PhoneNumber,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        BookingsCount = entity.Bookings.Count,
        LastPaymentAmount = entity.Payments
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefault()?.Amount ?? 0,
        LastPaymentDate = entity.Payments
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefault()?.PaymentDate,
        TotalPaidAmount = entity.Payments.Count != 0 ? entity.Payments.Sum(p => p.Amount) : 0
    };

}
