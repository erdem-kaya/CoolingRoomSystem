using Business.Factories;
using Business.Interfaces;
using Business.Models.CoolingRoom;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class CoolingRoomService(ICoolingRoomRepository coolingRoomRepository) : ICoolingRoomService
{
    private readonly ICoolingRoomRepository _coolingRoomRepository = coolingRoomRepository;

    public async Task<CoolingRoomForm> CreateAsync(CoolingRoomRegistrationForm form)
    {
        if (form == null) throw new ArgumentNullException(nameof(form), "Cooling Room form cannot be null.");

        await _coolingRoomRepository.BeginTransactionAsync();

        try
        {
            var coolingRoom = CoolingRoomFactory.Create(form);
            coolingRoom.CreatedAt = DateTime.UtcNow;

            var createdCoolingRoom = await _coolingRoomRepository.CreateAsync(coolingRoom);
            await _coolingRoomRepository.CommitTransactionAsync();

            if (createdCoolingRoom == null) throw new Exception("Error creating cooling room.");
            return CoolingRoomFactory.Create(createdCoolingRoom);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating cooling room: {ex.Message}");
            await _coolingRoomRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<IEnumerable<CoolingRoomForm>> GetAllAsync()
    {
        try
        {
            var coolingRooms = await _coolingRoomRepository.GetAllAsync();
            var result = coolingRooms.Select(CoolingRoomFactory.Create).ToList() ?? [];
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting cooling rooms: {ex.Message}");
            return [];
        }
    }

    public async Task<CoolingRoomForm> GetByIdAsync(int id)
    {
        try
        {
            var coolingRoom = await _coolingRoomRepository.GetItemAsync(x => x.Id == id);
            var result = coolingRoom != null ? CoolingRoomFactory.Create(coolingRoom) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting cooling room by id: {ex.Message}");
            return null!;
        }
    }

    public async Task<CoolingRoomForm> UpdateAsync(CoolingRoomUpdateForm form)
    {
        await _coolingRoomRepository.BeginTransactionAsync();

        try
        {
            var findCoolingRoom = await _coolingRoomRepository.GetItemAsync(x => x.Id == form.Id) 
                ?? throw new Exception($"Cooling room with ID {form.Id} does not exist.");
            
            findCoolingRoom.UpdatedAt = DateTime.UtcNow;
            CoolingRoomFactory.Update(findCoolingRoom, form);
            
            var updatedCoolingRoom = await _coolingRoomRepository.UpdateAsync(x => x.Id == form.Id, findCoolingRoom);
            var result = updatedCoolingRoom != null ? CoolingRoomFactory.Create(updatedCoolingRoom) : null!;
            await _coolingRoomRepository.CommitTransactionAsync();
            return result;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating cooling room: {ex.Message}");
            await _coolingRoomRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _coolingRoomRepository.DeleteAsync(x => x.Id == id);
            if (!deleted) throw new Exception($"Cooling room with ID {id} does not exist.");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting cooling room: {ex.Message}");
            return false;
        }
    }



    public async Task<int> GetAvailableCoolingRoomCountAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var coolingRoomCount = await _coolingRoomRepository.GetAvailableCoolingRoomCountAsync(startDate, endDate);
            return coolingRoomCount;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting available cooling room count: {ex.Message}");
            return 0;
        }
    }

    public async Task<IEnumerable<CoolingRoomEntity>> GetAvailableCoolingRoomsAsync()
    {
        try
        {
            var coolingRooms = await _coolingRoomRepository.GetAvailableCoolingRoomsAsync();
            return coolingRooms;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting available cooling rooms: {ex.Message}");
            return [];
        }
    }

    public async Task<CoolingRoomEntity?> SetCoolingRoomPriceAsync(int coolingRoomId, int coolingRoomPriceId)
    {
        try
        {
            var coolingRoom = await _coolingRoomRepository.GetItemAsync(x => x.Id == coolingRoomId) ?? throw new Exception($"Cooling room with ID {coolingRoomId} does not exist.");
            coolingRoom.RoomPriceId = coolingRoomPriceId;
            var updatedCoolingRoom = await _coolingRoomRepository.UpdateAsync(x => x.Id == coolingRoomId, coolingRoom);
            return updatedCoolingRoom;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error setting cooling room price: {ex.Message}");
            return null!;
        }
    }

    public async Task UpdateAllCoolingRoomPriceAsync(int coolingRoomPrice)
    {
        await _coolingRoomRepository.BeginTransactionAsync();
        try
        {
            var coolingRooms = await _coolingRoomRepository.GetAllAsync();
            foreach (var coolingRoom in coolingRooms)
            {
                coolingRoom.RoomPriceId = coolingRoomPrice;
                var updatePrice = _coolingRoomRepository.UpdateAsync(x => x.Id == coolingRoom.Id, coolingRoom) ?? throw new Exception("Error updating cooling room price.");  
            }
            await _coolingRoomRepository.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating all cooling room prices: {ex.Message}");
            await _coolingRoomRepository.RollbackTransactionAsync();
        }
    }

   

    public async Task UpdateCoolingRoomPriceForDateRangeAsync(int coolingRoomPriceId, DateTime startDate, DateTime endDate)
    {
       try
        {
            
            var coolingRooms = await _coolingRoomRepository.GetAllAsync(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate);
            foreach (var coolingRoom in coolingRooms)
            {
                coolingRoom.RoomPriceId = coolingRoomPriceId;
                var updatePrice = _coolingRoomRepository.UpdateAsync(x => x.Id == coolingRoom.Id, coolingRoom) ?? throw new Exception("Error updating cooling room price for date range.");
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating cooling room price for date range: {ex.Message}");
        }
    }

}
