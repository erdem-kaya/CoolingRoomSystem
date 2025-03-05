using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class PaymentStatusRepository(DataContext context) : BaseRepository<PaymentStatusEntity>(context), IPaymentStatusRepository
{
}
