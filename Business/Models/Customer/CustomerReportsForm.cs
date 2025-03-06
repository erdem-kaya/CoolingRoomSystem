using System.ComponentModel.DataAnnotations;

namespace Business.Models.Customer;

public class CustomerReportsForm
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
    public string? Email { get; set; }
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Geçerli bir telefon numarası girin.")]
    public string PhoneNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int BookingsCount { get; set; }
    public decimal LastPaymentAmount { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public decimal TotalPaidAmount { get; set; }
}
