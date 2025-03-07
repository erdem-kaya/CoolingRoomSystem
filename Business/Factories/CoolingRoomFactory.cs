using Business.Models.CoolingRoom;
using Data.Entities;

namespace Business.Factories;

public class CoolingRoomFactory
{
    public static CoolingRoomEntity Create(CoolingRoomRegistrationForm form) => new()
    {
        RoomName = form.RoomName,
        RoomStatusId = form.RoomStatus,
        RoomPrice = new CoolingRoomPriceControlEntity { UnitPrice = form.UnitPrice }
    };


    public static CoolingRoomForm Create(CoolingRoomEntity entity) => new()
    {
        Id = entity.Id,
        RoomName = entity.RoomName,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        RoomStatus = entity.RoomStatus.StatusName,
        UnitPrice = entity.RoomPrice.UnitPrice
    };

    public static void Update(CoolingRoomEntity entity, CoolingRoomUpdateForm form)
    {
        var newPrice = new CoolingRoomPriceControlEntity { UnitPrice = form.UnitPrice };

        entity.RoomName = form.RoomName ?? entity.RoomName; // Eğer yeni isim girilmediyse eskisini koru.
        entity.RoomStatusId = form.RoomStatus != 0 ? form.RoomStatus : entity.RoomStatusId; // Eğer yeni status girilmediyse eskisini koru.
        entity.RoomPrice = newPrice; // ID yerine nesneyi ata
        entity.UpdatedAt = DateTime.UtcNow;
    }

}

