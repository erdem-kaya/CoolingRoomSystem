using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomersEntity>(context), ICustomerRepository
{

}
