using Business.Factories;
using Business.Interfaces;
using Business.Models.Customer;
using Business.Models.Payment;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class PaymentService(IPaymentRepository paymentRepository) : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;

    public async Task<PaymentForm> CreateAsync(PaymentRegistrationForm form)
    {
        if (form == null)
            throw new ArgumentNullException(nameof(form), "Payment form cannot be null.");
        await _paymentRepository.BeginTransactionAsync();

        try
        {
            var payment = PaymentFactory.Create(form);
            var createdPayment = await _paymentRepository.CreateAsync(payment);
            var result = createdPayment != null ? PaymentFactory.Create(createdPayment) : null!;
            await _paymentRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating payment: {ex.Message}");
            await _paymentRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<IEnumerable<PaymentForm>> GetAllAsync()
    {
        try
        {
            var allPayments = await _paymentRepository.GetAllAsync();
            var result = allPayments.Select(PaymentFactory.Create).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all payments: {ex.Message}");
            return [];
        }
    }

    public async Task<PaymentForm> GetByIdAsync(int id)
    {
        try
        {
            var payments = await _paymentRepository.GetItemAsync(x => x.Id == id);
            var result = payments != null ? PaymentFactory.Create(payments) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting payment by ID: {ex.Message}");
            return null!;
        }
    }

    public async Task<PaymentForm> UpdateAsync(PaymentUpdateForm form)
    {
        await _paymentRepository.BeginTransactionAsync();

        try
        {
            var findPayment = await _paymentRepository.GetItemAsync(x => x.Id == form.Id) ?? throw new Exception($"Payment with ID {form.Id} not found.");
            PaymentFactory.Update(findPayment, form);
            var updatedPayment = await _paymentRepository.UpdateAsync(x => x.Id == form.Id, findPayment);
            var result = updatedPayment != null ? PaymentFactory.Create(updatedPayment) : null!;
            await _paymentRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating payment: {ex.Message}");
            await _paymentRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var deletedPayment = await _paymentRepository.DeleteAsync(x => x.Id == id);
            if (!deletedPayment)
                throw new Exception($"Payment with ID {id} not found.");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting payment: {ex.Message}");
            return false;
        }
    }

    public async Task<decimal> CalculateRemainingDebtAsync(int customerId)
    {
        try
        {
            var remainingDebt = await _paymentRepository.CalculateRemainingDebtAsync(customerId);
            return remainingDebt;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error calculating remaining debt: {ex.Message}");
            return 0;
        }
    }

    public async Task<PaymentReportForm> GetPaymentByBookingIdAsync(int bookingId)
    {
        try
        {
            var getPaymentByBookingId = await _paymentRepository.GetPaymentByBookingIdAsync(bookingId);
            var result = getPaymentByBookingId != null ? PaymentFactory.CreateReports(getPaymentByBookingId) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting payment by booking ID: {ex.Message}");
            return null!;
        }
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        try
        {
            var getTotalRevenue = await _paymentRepository.GetTotalRevenueAsync();
            return getTotalRevenue;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting total revenue: {ex.Message}");
            return 0;
        }
    }


}
