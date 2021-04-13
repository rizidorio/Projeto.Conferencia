using Domain.Entity;
using Domain.Interface.Repository;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConferenciaContext _context;
        private readonly DbSet<User> users;

        public UserRepository(ConferenciaContext context)
        {
            _context = context;
            users = _context.Set<User>();
        }

        public async Task<User> GetByLogin(string login)
        {
            return await users.FirstOrDefaultAsync(u => u.Login.Equals(login));
        }

        public async Task<User> New(User user)
        {
            EntityEntry<User> result = await users.AddAsync(user);

            if (result is not null)
            {
                await _context.SaveChangesAsync();
                return user;
            }

            return null;
        }

        public async Task<User> Update(User user)
        {
            EntityEntry<User> result = users.Update(user);

            if (result is not null)
            {
                await _context.SaveChangesAsync();
                return user;
            }

            return null;
        }
    }
}
