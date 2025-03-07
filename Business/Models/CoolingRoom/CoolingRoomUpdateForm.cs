namespace Business.Models.CoolingRoom;

public class CoolingRoomUpdateForm
{
    public int Id { get; set; }
    public string? RoomName { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int RoomStatus { get; set; }
    public decimal UnitPrice { get; set; }
}
