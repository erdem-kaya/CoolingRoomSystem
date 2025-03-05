using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CoolingRoomEntity
{
    public int Id { get; set; }
    public string RoomName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int RoomStatusId { get; set; }
    public CoolingRoomStatusEntity RoomStatus { get; set; } = null!;
    public int RoomPriceId { get; set; }
    public CoolingRoomPriceControlEntity RoomPrice { get; set; } = null!;
}
