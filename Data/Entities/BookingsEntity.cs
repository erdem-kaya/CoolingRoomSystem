using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class BookingsEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime BookingDate { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime FinishDate { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [Required]
    public string Description { get; set; } = null!;

    public int? BookedByUserId { get; set; }
    public UsersEntity? BookedByUser { get; set; }

    [Required]
    public int PaymentId { get; set; }
    public PaymentsEntity Payment { get; set; } = null!;

    [Required]
    public int CustomerId { get; set; }
    public CustomersEntity Customer { get; set; } = null!;

    [Required]
    public int CoolingRoomId { get; set; }
    public CoolingRoomEntity CoolingRoom { get; set; } = null!;
}
