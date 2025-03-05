using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CoolingRoomPriceControlRepository(DataContext context) : BaseRepository<CoolingRoomPriceControlEntity>(context), ICoolingRoomPriceControlRepository
{

}
