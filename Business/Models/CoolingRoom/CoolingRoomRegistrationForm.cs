namespace Business.Models.CoolingRoom;

public class CoolingRoomRegistrationForm
{
    public string RoomName { get; set; } = null!;
    public int RoomStatus { get; set; }
    public decimal UnitPrice { get; set; }
}
