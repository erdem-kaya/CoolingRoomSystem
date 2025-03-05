using Data.Entities;

namespace Data.Interfaces; 

public interface ICoolingRoomRepository : IBaseRepository<CoolingRoomEntity>
{
    // Get available rooms
    // Kullanilabilir odalari getir
    Task<IEnumerable<CoolingRoomEntity>> GetAvailableCoolingRoomsAsync();

    // Number of available Cooling rooms between selected dates
    // Secilen tarihler arasindaki kullanilabilir soğutma odalarinin sayisi
    Task<int> GetAvailableCoolingRoomCountAsync(DateTime startDate, DateTime endDate);

    // Set CoolingRooms price
    // Soğutma odalarinin fiyatini belirle
    Task<CoolingRoomEntity?> SetCoolingRoomPriceAsync(int coolingRoomId, int coolingRoomPriceId);

    // Update all CoolingRooms Price 
    // Tüm soğutma odalarinin fiyatini güncelle
    Task UpdateAllCoolingRoomPriceAsync(int coolingRoomPrice);


    // Update Cooling Room price for a specific date range
    // Belirli bir tarih araligi icin soğutma odasi fiyatini güncelle
    Task UpdateCoolingRoomPriceForDateRangeAsync(int coolingRoomPriceId, DateTime startDate, DateTime endDate);
}
