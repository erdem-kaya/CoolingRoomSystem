using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class CoolingRoomRepository(DataContext context) : BaseRepository<CoolingRoomEntity>(context), ICoolingRoomRepository
{
    public async Task<int> GetAvailableCoolingRoomCountAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
           var count = await _context.CoolingRooms
                .Where(x => x.RoomStatusId == 1 && !_context.Bookings.Any(b => b.CoolingRoomId == x.Id && b.FinishDate > DateTime.UtcNow))
                .CountAsync();
            return count;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting available cooling room count : {ex.Message}");
            return 0;
        }
    }

    public async Task<IEnumerable<CoolingRoomEntity>> GetAvailableCoolingRoomsAsync()
    {
        try
        {
            var availableRooms = await _context.CoolingRooms
                .Where(x => x.RoomStatusId == 1)
                .ToListAsync();

            return availableRooms;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting available cooling rooms : {ex.Message}");
            return [];
        }
    }

    public async Task<CoolingRoomEntity?> SetCoolingRoomPriceAsync(int coolingRoomId, int coolingRoomPriceId)
    {
        try
        {
            var coolingRoom = await _context.CoolingRooms
                .FirstOrDefaultAsync(x => x.Id == coolingRoomId);
            if (coolingRoom == null)
                return null!;

            var coolingRoomPrice = await _context.CoolingRoomPriceControl
                .FirstOrDefaultAsync(x => x.Id == coolingRoomPriceId);
            if (coolingRoomPrice == null)
                return null!;

            coolingRoom.RoomPrice = coolingRoomPrice;
            
            await _context.SaveChangesAsync();
            return coolingRoom;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error setting cooling room price : {ex.Message}");
            return null!;
        }
    }

    public async Task UpdateAllCoolingRoomPriceAsync(int coolingRoomPriceId)
    {
        try
        {
            var coolingRoomPrice = await _context.CoolingRoomPriceControl
                .FirstOrDefaultAsync(x => x.Id == coolingRoomPriceId);
            if (coolingRoomPrice == null)
                return;

            var allRooms = await _context.CoolingRooms.ToListAsync();

            foreach (var room in allRooms)
            {
                room.RoomPrice = coolingRoomPrice;
            }

             await _context.SaveChangesAsync();
        
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating all cooling room prices : {ex.Message}");
        }
    }

    public async Task UpdateCoolingRoomPriceForDateRangeAsync(int coolingRoomPriceId, DateTime startDate, DateTime endDate)
    {
        try
        {
            var coolingRoomPrice = await _context.CoolingRoomPriceControl
                .FirstOrDefaultAsync(x => x.Id == coolingRoomPriceId);

            if (coolingRoomPrice == null)
                return;

            var rooms = await _context.CoolingRooms.ToListAsync();

            foreach (var room in rooms)
            {
                var roomReservations = await _context.Bookings
                    .Where(x => x.CoolingRoomId == room.Id && x.StartDate >= startDate && x.FinishDate <= endDate)
                    .ToListAsync();
                if (roomReservations.Count != 0)
                {
                    room.RoomPrice = coolingRoomPrice;
                }

            }
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating cooling room price for date range : {ex.Message}");
        }
    }
}
