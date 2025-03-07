namespace Business.Models.Payment;

public class PaymentUpdateForm
{
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string? PaymentMethod { get; set; } // Havale, EFT, Kredi Kartı, Nakit kontrolü
    public decimal? PrePayment { get; set; }
    public DateTime? PrePaymentDate { get; set; } // Eğer ön ödeme yapıldıysa ön ödeme tarihi
    public DateTime? ConfirmendAt { get; set; } // Ödemenin onaylandığı tarih
    public int PaymentStatus { get; set; } // Ödeme durumu.
    public int CustomerPaymentId { get; set; } // Müşteri Id
}
