using Azure.Core;
using Business.Models.Payment;
using Data.Entities;

namespace Business.Factories;

public class PaymentFactory
{
    public static PaymentsEntity Create(PaymentRegistrationForm form) => new()
    {
        PaymentDate = form.PaymentDate,
        Amount = form.Amount,
        PaymentMethod = form.PaymentMethod,
        PrePayment = form.PrePayment,
        ConfirmedAt = form.ConfirmedAt,
        PaymentStatusId = form.PaymentStatus,
        CustomerPaymentId = form.CustomerPaymentId
    };

    public static PaymentForm Create(PaymentsEntity entity) => new()
    {
        Id = entity.Id,
        PaymentDate = entity.PaymentDate,
        Amount = entity.Amount,
        PaymentMethod = entity.PaymentMethod,
        PrePayment = entity.PrePayment,
        ConfirmedAt = entity.ConfirmedAt,
        PaymentStatus = entity.PaymentStatus.Id,
        CustomerPaymentId = entity.Customers.Id
    };

    public static void Update(PaymentsEntity entity, PaymentUpdateForm form)
    {
        if (form.PaymentDate != entity.PaymentDate)
            entity.PaymentDate = form.PaymentDate;
        if (form.Amount != entity.Amount)
            entity.Amount = form.Amount;
        if (form.PaymentMethod != entity.PaymentMethod)
            entity.PaymentMethod = form.PaymentMethod;
        if (form.PrePayment != entity.PrePayment)
            entity.PrePayment = form.PrePayment;
        if (form.ConfirmedAt != entity.ConfirmedAt)
            entity.ConfirmedAt = form.ConfirmedAt;
        if (form.PaymentStatus != entity.PaymentStatus.Id)
            entity.PaymentStatusId = form.PaymentStatus;
        if (form.CustomerPaymentId != entity.Customers.Id)
            entity.CustomerPaymentId = form.CustomerPaymentId;
    }

    public static PaymentReportForm CreateReports(PaymentsEntity entity) => new()
    {
        Id = entity.Id,
        PaymentDate = entity.PaymentDate,
        Amount = entity.Amount,
        PaymentMethod = entity.PaymentMethod,
        PrePayment = entity.PrePayment,
        ConfirmedAt = entity.ConfirmedAt,
        PaymentStatus = entity.PaymentStatus?.Id ?? 0,
        CustomerPaymentId = entity.Customers?.Id ?? 0,
    };
}
