using System.ComponentModel.DataAnnotations;

namespace Business.Models.Customer;

public class CustomerRegistrationForm
{
    [Required(ErrorMessage = "İsim alanı zorunludur.")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "Soyisim alanı zorunludur.")]
    public string LastName { get; set; } = null!;
    [Required(ErrorMessage = "Adres alanı zorunludur.")]
    public string Address { get; set; } = null!;
    [Required(ErrorMessage = "Şehir alanı zorunludur.")]
    public string City { get; set; } = null!;
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
    public string? Email { get; set; }
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Geçerli bir telefon numarası girin.")]
    public string PhoneNumber { get; set; } = null!;
}
