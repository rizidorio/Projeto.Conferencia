using Domain.Entity;
using Domain.Interface.Repository;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ConferenciaContext _context;
        private readonly DbSet<Product> products;

        public ProductRepository(ConferenciaContext context)
        {
            _context = context;
            products = _context.Set<Product>();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await products.ToListAsync();
        }

        public async Task<Product> GetByCode(int code)
        {
            return await products.FirstOrDefaultAsync(p => p.Code.Equals(code));
        }

        public async Task<Product> New(Product product)
        {
            EntityEntry<Product> result = await products.AddAsync(product);

            if (result is not null)
            {
                await _context.SaveChangesAsync();
                return product;
            }

            return null;
        }

        public async Task<Product> Update(Product product)
        {
            EntityEntry<Product> result = products.Update(product);

            if (result is not null)
            {
                await _context.SaveChangesAsync();
                return product;
            }

            return null;
        }
    }
}
