using Domain.Entity;
using System.Threading.Tasks;

namespace Domain.Interface.Repository
{
    public interface IUserRepository
    {
        Task<User> New(User user);
        Task<User> Update(User user);
        Task<User> GetByLogin(string login);
        Task<bool> Exists(User user);
    }
}
