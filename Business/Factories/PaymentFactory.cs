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
        PaymentStatus = new PaymentStatusEntity { Id = form.PaymentStatus },
        Customers = new CustomersEntity { Id = form.CustomerPaymentId }
    };
}
