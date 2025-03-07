namespace Business.Models.CoolingRoom;

public class CoolingRoomReportsForm
{
    public int Id { get; set; }
    public string RoomName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? RoomStatus { get; set; }
    public decimal UnitPrice { get; set; }
}
