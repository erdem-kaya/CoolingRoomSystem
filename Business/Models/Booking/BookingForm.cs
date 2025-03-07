﻿namespace Business.Models.Booking;

public class BookingForm
{
    public int Id { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateDate { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
    public int PaymentId { get; set; }
    public string Description { get; set; } = null!;
    public int BookedByUserId { get; set; }
    public int CustomerId { get; set; }
    public int CoolingRoomId { get; set; }
}
