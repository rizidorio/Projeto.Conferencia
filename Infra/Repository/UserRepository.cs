using Domain.Entity;
using Domain.Interface.Repository;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> Exists(User user)
        {
            var result = await GetById(user.Id);

            if (result.Login.Equals(user.Login))
            {
                return true;
            }

            return false;

        }

        public async Task<User> GetById(int id)
        {
            return await users.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<User> New(User user)
        {
            var result = await users.AddAsync(user);

            if (result is not null)
            {
               await _context.SaveChangesAsync();
                return user;
            }

            return null;
        }

        public async Task<User> Update(User user)
        {
            var result = users.Update(user);

            if (result is not null)
            {
               await _context.SaveChangesAsync();
                return user;
            }

            return null;
        }
    }
}
