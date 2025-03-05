using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CoolingRoomStatusRepository(DataContext context) : BaseRepository<CoolingRoomStatusEntity>(context), ICoolingRoomStatusRepository
{
}
