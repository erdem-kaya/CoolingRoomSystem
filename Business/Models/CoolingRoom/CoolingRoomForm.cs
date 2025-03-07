using Data.Entities;

namespace Business.Models.CoolingRoom;

public class CoolingRoomForm
{
    public int Id { get; set; }
    public string RoomName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string RoomStatus { get; set; } = null!;
    public decimal UnitPrice { get; set; }
}

