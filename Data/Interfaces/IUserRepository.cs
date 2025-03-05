using Data.Entities;

namespace Data.Interfaces;

public interface IUserRepository : IBaseRepository<UsersEntity>
{
    // Compare password hash for user authentication
    // Kullanici kimligi ve sifresi ile kullaniciyi dogrulamak icin kullanilir
    Task<UsersEntity?> AuthenticateAsync(string email);
}
